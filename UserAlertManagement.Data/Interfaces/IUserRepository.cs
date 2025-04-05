using UserAlertManagement.Data.Models;

namespace UserAlertManagement.Data.Interfaces;

public interface IUserRepository
{
    Task<User> GetUser(Guid userId);
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(Guid user);
}