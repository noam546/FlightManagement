namespace UserAlertManagement.Data.Models;

public class Alert
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } 
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public string FromAirport { get; set; }
    public string ToAirport { get; set; }
    public decimal MaxPrice { get; set; }
    public DateTime DepartureDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsOn { get; set; }
}