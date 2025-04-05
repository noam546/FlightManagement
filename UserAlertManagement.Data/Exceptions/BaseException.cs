namespace UserAlertManagement.Data.Exceptions;

public abstract class BaseException : Exception
{
    public BaseException(string message)
        : base(message)
    {
    }

    protected BaseException()
    {
    }

    public abstract int StatusCode { get; }
}