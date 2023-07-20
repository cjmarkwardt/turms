namespace Markwardt.Turms;

public interface IConnection : ITransmitter, IMultiDisposable
{
    IReadOnlyValue<string?> Disconection { get; }
    IObservable<object> ReceivedMessage { get; }

    void Send(object message);
    void Start();
}

public class Connection : IConnection
{
    public static async ValueTask<Failable<Connection>> Create(IConnector connector, IConnectionProcessor processor, IConnectionHandler handler, CancellationToken cancellation = default)
    {
        Connection connection = new(processor, handler);
        
        LinkHandler linkHandler = new();
        linkHandler.Broken.Subscribe(x => handler.TriggerDisconnected(x));
        linkHandler.ReceivedData.Subscribe(x => processor.Receive(x));

        Failable<ILink> tryCreateLink = await connector.Connect(linkHandler, cancellation);
        if (tryCreateLink.Failure != null)
        {
            return tryCreateLink.Failure;
        }
        
        connection.link = tryCreateLink.Result;
        return connection;
    }
    
    private Connection(ILink link, IConnectionProcessor processor)
    {
        this.link = link;
        this.processor = processor;
    }

    private readonly ILink link;
    private readonly IConnectionProcessor processor;

    private bool isStartInitiated;
    private bool isStarted;

    private readonly ObservableValue<string> disconnection = new();
    public IObservableValue<string> Disconection => disconnection;
    
    private readonly Subject<object> receivedMessage = new();
    public IObservable<object> ReceivedMessage => receivedMessage;

    public int TransmitPriority => processor.Output.Priority;
    public int ReceivePriority => processor.Input.Priority;

    public void Send(object message, bool reliable = true)
        => processor.Send(message);

    public void Start()
    {
        if (!isStartInitiated)
        {
            isStartInitiated = true;

            if (link.Disconnection.Value == null)
            {
                link.Disconn
                
                while (processor.Input.HasNext)
                {
                    Receive();
                }
            }
            else
            {
                disconnection.Set(link.Disconnection);
                disconnected.OnNext(link.Disconnection);
            }
            
            isStarted = true;
        }
    }

    public void Transmit()
    {
        if (processor.Output.HasNext)
        {
            link.Send(processor.Output.Next());
        }
    }

    public void Receive()
    {
        if (processor.Input.HasNext)
        {
            handler.TriggerReceivedMessage(processor.Input.Next());
        }
    }

    public void Dispose()
        => link.Dispose();

    public async ValueTask DisposeAsync()
        => await link.DisposeAsync();
}