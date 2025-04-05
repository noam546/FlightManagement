using UserAlertManagement.Services.Models;

namespace UserAlertManagement.Services.Interfaces;

public interface IUserAlertService
{
    Task<UserModel> CreateUser(UserModel user);
    Task<UserModel> UpdateUser(UserModel user);
    Task DeleteUser(Guid userId);
    Task<UserModel> GetUser(Guid userId);
    Task DeleteAlert(Guid alertId);
    Task<AlertModel> GetAlert(Guid alertId);
    Task<AlertModel> CreateAlert(AlertModel alert);
    Task<AlertModel> UpdateAlert(AlertModel alert);
    Task<List<UserModel>> GetUsersMatchingAlerts(string from, string to, DateTime? departureDate, decimal? price);
}