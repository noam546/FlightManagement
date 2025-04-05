using AutoMapper;
using UserAlertManagement.Data.Context;
using UserAlertManagement.Data.Exceptions;
using UserAlertManagement.Data.Interfaces;
using UserAlertManagement.Data.Models;

namespace UserAlertManagement.Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User> GetUser(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with id: {userId} was not found.");
        }
        return user;
    }

    public async Task<User> AddUser(User user)
    {
        var userDb = await _context.Users.FindAsync(user.Id);
        if (userDb != null)
        {
            throw new UserAlreadyExistsException($"User with id: {user.Id} already exists.");
        }
        user.CreatedAt = DateTime.UtcNow;
        user.LastUpdatedAt = DateTime.UtcNow;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        var userdb = await _context.Users.FindAsync(user.Id);
        if (userdb == null)
        {
            throw new UserNotFoundException($"User with id: {user.Id} was not found.");
        }
        _mapper.Map(user, userdb);
        userdb.LastUpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUser(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with id: {user.Id} was not found.");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

}