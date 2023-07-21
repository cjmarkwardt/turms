namespace Markwardt.Turms;

public interface IReadOnlyValue<T>
{
    T Get();
    IObservable<T> Observe(bool initialValue = true);
}