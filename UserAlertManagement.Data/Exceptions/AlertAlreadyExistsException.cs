using System.Net;

namespace UserAlertManagement.Data.Exceptions;

public class AlertAlreadyExistsException : BaseException
{
    public AlertAlreadyExistsException(string message) : base(message)
    {
    }
    
    public override int StatusCode => (int) HttpStatusCode.Conflict;
}        
