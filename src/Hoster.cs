namespace Markwardt.Turms;

public interface IHoster
{
    ValueTask<Failable<IListener>> Host(IListenerHandler handler, CancellationToken cancellation = default);
}

public static class HosterUtils
{
    public static async ValueTask<Failable<IServer>> Host(IServerHandler handler, CancellationToken cancellation = default)
        => null!;
}