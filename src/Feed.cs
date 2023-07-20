namespace Markwardt.Turms;

public interface IOutput<T>
{
    bool TryNext([NotNullWhen(true)] T? value);
}