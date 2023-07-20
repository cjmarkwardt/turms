namespace Markwardt.Turms;

public interface IConnectionProcessor
{
    IStream<ReadOnlyMemory<byte>> Output { get; }
    IStream<object> Input { get; }

    void Send(object message);
    void Receive(ReadOnlyMemory<byte> block);

    interface IStream<T>
    {
        int Priority { get; }
        bool HasNext { get; }

        T Next();
    }
}