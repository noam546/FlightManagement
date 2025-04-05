using System.Net;

namespace UserAlertManagement.Data.Exceptions;

public class UserNotFoundException : BaseException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
    
    public override int StatusCode => (int) HttpStatusCode.NotFound;
}        
