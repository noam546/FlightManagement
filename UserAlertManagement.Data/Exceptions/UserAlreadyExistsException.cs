using System.Net;

namespace UserAlertManagement.Data.Exceptions;

public class UserAlreadyExistsException : BaseException
{
    public UserAlreadyExistsException(string message) : base(message)
    {
    }
    
    public override int StatusCode => (int) HttpStatusCode.Conflict;
}        
