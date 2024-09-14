namespace GarbageC;

public class Result<T, E>
{
    public bool IsOk => _okValue.IsValueCreated;
    public bool IsError => _errValue.IsValueCreated;

    protected readonly Lazy<T> _okValue;
    protected readonly Lazy<E> _errValue;
    
    public static Result<T, E> Ok(T value)
    {
        return new Result<T, E>(value);
    }

    public static Result<T, E> Error(E error)
    {
        return new Result<T, E>(error);
    }
    
    /// <summary>
    /// Returns Error value if it is created, otherwise Ok value (in out parameter)
    /// </summary>
    /// <param name="value">Ok value (if Error value is not created)</param>
    /// <returns>Error value</returns>
    public E? GetError(out T? value)
    {
        if (_errValue.IsValueCreated)
        {
            value = default; // Null if T is the reference type
            return _errValue.Value;
        }
        value = _okValue.Value;
        return default;
    }
    
    /// <summary>
    /// Returns Ok value or if it is not created, returns value that's paseed in parameter
    /// </summary>
    /// <param name="defaultValue">Default value</param>
    /// <returns>Ok value or defaultValue parameter</returns>
    public T Value_Or(T defaultValue)
        => _okValue.IsValueCreated ? _okValue.Value : defaultValue;
    
    /// <summary>
    /// Returns Ok value or if it is not created, the value that's returned by passed function
    /// </summary>
    /// <param name="defaultValue">Function, which returns default value</param>
    /// <returns>Ok value or value, returned by passed function (defaultValue parameter)</returns>
    public T Value_Or_Else(Func<T> defaultValue)
        => _okValue.IsValueCreated ? _okValue.Value : defaultValue();
    
    protected Result(T value)
    {
        _okValue = new Lazy<T>(value);
        _errValue = new Lazy<E>();
    }

    protected Result(E error)
    {
        _errValue = new Lazy<E>(error);
        _okValue = new Lazy<T>();
    }
}

public class Result<T> : Result<T, ApplicationException>
{
    public T GetValueOrThrow() => !_okValue.IsValueCreated ? throw _errValue.Value : _okValue.Value;
    
    public new static Result<T> Ok(T value)
    {
        return new Result<T>(value);
    }

    public new static Result<T> Error(ApplicationException error)
    {
        return new Result<T>(error);
    }
    
    public static Result<T> Error(string error)
    {
        return new Result<T>(new ApplicationException(error));
    }
    
    protected Result(T value) : base(value) { }

    protected Result(ApplicationException error) : base(error) { }
}