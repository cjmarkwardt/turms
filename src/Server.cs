namespace Markwardt.Turms;

public interface IServer : IMultiDisposable
{
    IObservable<IConnectionRequest> Connected { get; }
}

public class Server : IServer
{
    public static async ValueTask<Failable<Server>> Create(IHoster hoster, IServerHandler handler, CancellationToken cancellation = default)
    {
        Server server = new(handler);

        ListenerHandler listenerHandler = new();

        Failable<IListener> tryHost = await hoster.Host(listenerHandler, cancellation);
        if (tryHost.Failure != null)
        {
            return tryHost.Failure;
        }

        server.listener = tryHost.Result;
        return server;
    }
    
    private Server(IServerHandler handler)
    {
        this.handler = handler;
    }

    private readonly IServerHandler handler;

    private IListener listener = null!;

    public IObservable<IConnectionRequest> Connected => handler.Connected;

    public void Dispose()
        => listener.Dispose();

    public async ValueTask DisposeAsync()
        => await listener.DisposeAsync();

    private void OnLinked(ILinkRequest request)
    {

    }

    private class Request : IConnectionRequest
    {
        public Request(ILinkRequest request)
        {
            this.request = request;
        }

        private readonly ILinkRequest request;

        public IConnection Accept(IConnectionProcessor processor, IConnectionHandler handler)
            => request.Accept();

        public void Reject()
            => request.Reject();
    }
}