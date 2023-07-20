namespace Markwardt.Turms;

public interface ILinkHandler
{
    IObservable<string> Broken { get; }
    IObservable<ReadOnlyMemory<byte>> ReceivedData { get; }

    void TriggerBroken(string reason);
    void TriggerReceivedData(ReadOnlyMemory<byte> data);
}

public class LinkHandler : ILinkHandler
{
    private readonly Subject<string> broken = new();
    public IObservable<string> Broken => broken;
    
    private readonly Subject<ReadOnlyMemory<byte>> receivedData = new();
    public IObservable<ReadOnlyMemory<byte>> ReceivedData => receivedData;

    public void TriggerBroken(string reason)
        => broken.OnNext(reason);

    public void TriggerReceivedData(ReadOnlyMemory<byte> data)
        => receivedData.OnNext(data);
}