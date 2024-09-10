namespace CCCP;

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
    
    // Only for switch operator
    protected static Result<T, E> OkDefault()
    {
        return new Result<T, E>(default(T));
    }

    // Only for switch operator
    protected static Result<T, E> ErrorDefault()
    {
        return new Result<T, E>(default(E));
    }
    
    /// <summary>
    /// Returns Error value if it is created, otherwise Ok value (in out parameter)
    /// </summary>
    /// <param name="value">Ok value (if Error value is not created)</param>
    /// <returns>Error value</returns>
    public E? Error(out T? value)
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