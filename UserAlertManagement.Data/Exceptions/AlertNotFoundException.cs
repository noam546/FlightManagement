using System.Net;

namespace UserAlertManagement.Data.Exceptions;

public class AlertNotFoundException : BaseException
{
    public AlertNotFoundException(string message) : base(message)
    {
    }
    
    // public AlertNotFoundException() : base(message)
    // {
    // }
    
    public override int StatusCode => (int) HttpStatusCode.NotFound;
}        
