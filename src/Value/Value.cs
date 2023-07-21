namespace Markwardt.Turms;

public interface IValue<T> : IReadOnlyValue<T>
{
    void Set(T value);
}

public static class Value
{
    public static IValue<T> Create<T>(T value)
        => new Value<T>(value);

    public static IReadOnlyValue<T> Create<T>(T value, IObservable<T> changed)
        => new ObservedValue<T>(value, changed);

    public static IReadOnlyValue<TSelected> Select<T, TSelected>(this IReadOnlyValue<T> value, Func<T, TSelected> selector)
        => Create(selector(value.Get()), value.Observe(false).Select(selector));

    public static IReadOnlyValue<T> Where<T>(this IReadOnlyValue<T> value, Func<T, bool> filter)
        => Create(value.Get(), value.Observe(false).Where(filter));
}

public interface IProxySubject<T> : ISubject<T>
{
    void Set(IObservable<T>? target);
}

public class ProxySubject<T> : ISubject<T>, IDisposable
{
    public ProxySubject(IObservable<T>? target = null)
    {
        Set(target);
    }

    private readonly Subject<T> changed = new();
    
    private IDisposable? subscription;

    public void Set(IObservable<T>? target)
    {
        if (subscription != null)
        {
            subscription.Dispose();
            subscription = null;
        }

        if (target != null)
        {
            subscription = target.Subscribe(x => changed.OnNext(x));
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
        => changed.Subscribe(observer);

    public void Dispose()
        => subscription?.Dispose();

    public void OnCompleted()
        => changed.OnCompleted();

    public void OnError(Exception error)
        => changed.OnError(error);

    public void OnNext(T value)
        => changed.OnNext(value);
}

public class Value<T> : IValue<T>
{
    public Value(T value, IObservable<T>? changed = null)
    {
        this.value = value;
        this.changed = new(changed);
    }

    private readonly ProxySubject<T> changed;

    private T value;

    public T Get()
        => value;

    public void Set(T value)
    {
        this.value = value;
        changed.OnNext(value);
    }

    public IObservable<T> Observe(bool initial = true)
        => initial ? changed.Prepend(Get()) : changed;
}