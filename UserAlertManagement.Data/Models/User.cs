namespace UserAlertManagement.Data.Models;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string DeviceToken { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime LastUpdatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}