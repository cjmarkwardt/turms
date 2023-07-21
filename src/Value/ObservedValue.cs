namespace Markwardt.Turms;

public interface IObservedValue<T> : IReadOnlyValue<T>
{
    void Set()
}

public class ObservedValue<T> : IReadOnlyValue<T>, IDisposable
{
    public ObservedValue(T value, IObservable<T> changed)
    {
        this.value = value;
        this.changed = changed;
        subscription = changed.Subscribe(x => this.value = x);
    }

    private readonly IObservable<T> changed;
    private readonly IDisposable subscription;
    
    private T value;

    public T Get()
        => value;

    public IObservable<T> Observe(bool initial = true)
        => initial ? changed.Prepend(Get()) : changed;

    public void Dispose()
        => subscription.Dispose();
}