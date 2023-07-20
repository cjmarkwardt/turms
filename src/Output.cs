namespace Markwardt.Turms;

public interface IOutput<T>
{
    bool TryPop([NotNullWhen(true)] out T? item);
}