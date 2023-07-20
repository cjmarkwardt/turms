namespace Markwardt.Turms;

public interface IServerHandler
{
    IObservable<IConnectionRequest> Connected { get; }

    void TriggerConnected(IConnectionRequest request);
}

public class ServerHandler : IServerHandler
{
    private readonly Subject<IConnectionRequest> connected = new();
    public IObservable<IConnectionRequest> Connected => connected;

    public void TriggerConnected(IConnectionRequest request)
        => connected.OnNext(request);
}