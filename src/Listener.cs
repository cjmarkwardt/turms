namespace Markwardt.Turms;

public interface IListener : IMultiDisposable
{
    IObservable<ILinkRequest> Linked { get; }
}