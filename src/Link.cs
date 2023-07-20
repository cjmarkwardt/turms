namespace Markwardt.Turms;

public interface ILink : IMultiDisposable
{
    IReadOnlyValue<string?> Disconnection { get; }
    IOutput<ReadOnlyMemory<byte>> Receptions { get; }

    void Send(ReadOnlyMemory<byte> data, bool reliable = true);
}