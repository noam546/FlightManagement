using UserAlertManagement.Data.Models;

namespace UserAlertManagement.Data.Interfaces;
public interface IAlertRepository
{
    Task<List<Alert>> GetAlertsByUserId(Guid userId);
    Task<Alert> GetAlert(Guid alertId);
    Task<Alert> AddAlert(Alert alert);
    Task<Alert> UpdateAlert(Alert alert);
    Task DeleteAlert(Guid alertId);
    Task<List<User>> GetUsersByMatchingAlerts(string from, string to, DateTime? departureDate, decimal? price);
}

