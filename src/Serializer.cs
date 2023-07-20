namespace Markwardt.Turms;

public interface ISerializer
{
    IEnumerable<byte[]> Serialize(object message);
}