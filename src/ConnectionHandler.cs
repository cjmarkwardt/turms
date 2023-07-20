namespace Markwardt.Turms;

public interface IConnectionHandler
{
    IObservable<string> Disconnected { get; }
    IObservable<object> ReceivedMessage { get; }

    void TriggerDisconnected(string reason);
    void TriggerReceivedMessage(object message);
}

public class ConnectionHandler : IConnectionHandler
{
    private readonly Subject<string> disconnected = new();
    public IObservable<string> Disconnected => disconnected;
    
    private readonly Subject<object> receivedMessage = new();
    public IObservable<object> ReceivedMessage => receivedMessage;

    public void TriggerDisconnected(string reason)
        => disconnected.OnNext(reason);

    public void TriggerReceivedMessage(object message)
        => receivedMessage.OnNext(message);
}