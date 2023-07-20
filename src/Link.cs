namespace Markwardt.Turms;

public interface ILink : IMultiDisposable
{
    string? Break { get; }
    
    IObservable<string> Broken { get; }
    IObservable<ReadOnlyMemory<byte>> ReceivedData { get; }

    void Send(ReadOnlyMemory<byte> data, bool reliable = true);
}