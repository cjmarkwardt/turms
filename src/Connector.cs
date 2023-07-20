namespace Markwardt.Turms;

public interface IConnector
{
    ValueTask<Failable<ILink>> Connect(ILinkHandler handler, CancellationToken cancellation = default);
}

public static class ConnectorUtils
{
    public static async ValueTask<Failable<IConnection>> Connect(IConnectionHandler handler, CancellationToken cancellation = default)
        => null!;
}