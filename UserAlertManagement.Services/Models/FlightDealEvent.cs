namespace UserAlertManagement.Services.Models;

public class FlightDealEvent
{
    public string FlightId { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime DepartureDate { get; set; }
    public decimal Price { get; set; }
    public string Airline { get; set; }
    public DateTime FoundAt { get; set; }
}

public class UserNotificationDto
{
    public Guid UserId { get; set; }
    public string DeviceToken { get; set; }
}

public class UsersToNotifyEvent
{
    public string FlightId { get; set; }
    public string Message { get; set; }
    public List<UserNotificationDto> Users { get; set; }
}