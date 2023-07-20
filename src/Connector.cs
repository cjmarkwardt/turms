namespace Markwardt.Turms;

public interface IConnector
{
    ValueTask<Failable<ILink>> Connect(ILinkHandler handler, CancellationToken cancellation = default);
}

public static class ConnectorUtils
{
    public static async ValueTask<Failable<IConnection>> Connect(this IConnector connector, IConnectionProcessor processor, IConnectionHandler handler, CancellationToken cancellation = default)
        => (await Connection.Create(connector, processor, handler, cancellation)).Cast<IConnection>();
}