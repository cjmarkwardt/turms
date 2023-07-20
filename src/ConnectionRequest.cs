namespace Markwardt.Turms;

public interface IConnectionRequest
{
    IConnection Accept(IConnectionProcessor processor, IConnectionHandler handler);
    void Reject();
}