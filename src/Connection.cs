namespace Markwardt.Turms;

public interface IConnection : IMultiDisposable
{
    string? Disconnection { get; }

    IObservable<string> Disconnected { get; }
    IObservable<object> ReceivedMessage { get; }

    void Send(object message, bool reliable = true);
}