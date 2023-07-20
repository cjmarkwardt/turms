namespace Markwardt.Turms;

public interface IReadOnlyValue<T>
{
    T Get();
    IObservable<T> Observe(bool initialValue = true);
}

public interface IValue<T> : IReadOnlyValue<T>
{
    void Set(T value);
}

public static class Value
{
    public static IValue<T> Create<T>(T value)
        => new Value<T>(value);

    public static IReadOnlyValue<T> Create<T>(T value, IObservable<T> changed)
        => new ReadOnlyValue<T>(value, changed);

    public static IReadOnlyValue<TSelected> Select<T, TSelected>(this IReadOnlyValue<T> value, Func<T, TSelected> selector)
        => Create(selector(value.Get()), value.Observe(false).Select(selector));

    public static IReadOnlyValue<T> Where<T>(this IReadOnlyValue<T> value, Func<T, bool> filter)
        => Create(value.Get(), value.Observe(false).Where(filter));
}

public class ReadOnlyValue<T> : IReadOnlyValue<T>, IDisposable
{
    public ReadOnlyValue(T value, IObservable<T> changed)
    {
        this.value = value;
        this.changed = changed;
        subscription = changed.Subscribe(x => Value = x);
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

public class Value<T> : IValue<T>
{
    public Value(T value)
    {
        this.value = value;
    }

    private readonly Subject<T> changed = new();
    
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