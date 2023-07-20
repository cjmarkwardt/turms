namespace Markwardt.Turms;

public interface IListenerHandler
{
    IObservable<ILinkRequest> Linked { get; }

    void TriggerLinked(ILinkRequest request);
}

public class ListenerHandler : IListenerHandler
{
    private readonly Subject<ILinkRequest> linked = new();
    public IObservable<ILinkRequest> Linked => linked;

    public void TriggerLinked(ILinkRequest request)
        => linked.OnNext(request);
}