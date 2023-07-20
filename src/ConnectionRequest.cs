namespace Markwardt.Turms;

public interface IConnectionRequest
{
    IConnection Accept(IConnectionHandler handler);
    void Reject();
}