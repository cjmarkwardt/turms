namespace Markwardt.Turms;

public interface ITransmitter
{
    int TransmitPriority { get; }
    int ReceivePriority { get; }
    
    void Transmit();
    void Receive();
}