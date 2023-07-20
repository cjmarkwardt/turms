namespace Markwardt.Turms;

public interface IServer : IMultiDisposable
{
    IObservable<IConnectionRequest> Connected { get; }
}

public class Server : IServer
{
    public Server(IConnector connector, IServerHandler handler)
    {
        this.handler = new ListenerHandler();
        this.handler.Linked.Subscribe(OnLinked);
    }

    private readonly IListenerHandler handler;

    private readonly Subject<IConnectionRequest> connected = new();
    public IObservable<IConnectionRequest> Connected => connected;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

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

        public IConnection Accept(IConnectionHandler handler)
            => request.Accept();

        public void Reject()
            => request.Reject();
    }

    private class Connection : IConnection
    {
        public Connection(ILink link, IConnectionHandler handler)
        {
            this.link = link;
            this.handler = handler;
        }

        private readonly ILink link;
        private readonly IConnectionHandler handler;

        public string? Disconnection => link.Break;

        public IObservable<string> Disconnected => throw new NotImplementedException();

        public IObservable<object> ReceivedMessage => throw new NotImplementedException();

        public void Send(object message, bool reliable = true)
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}