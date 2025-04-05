using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserAlertManagement.Data.Context;
using UserAlertManagement.Data.Exceptions;
using UserAlertManagement.Data.Interfaces;
using UserAlertManagement.Data.Models;

namespace UserAlertManagement.Data;

public class AlertRepository : IAlertRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public AlertRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Alert>> GetAlertsByUserId(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with id: {userId} was not found.");
        }
        return await _context.Alerts
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Alert> GetAlert(Guid alertId)
    {
        var alert = await _context.Alerts.FindAsync(alertId);
        if (alert == null)
        {
            throw new AlertNotFoundException($"Alert with id: {alertId} was not found.");
        }

        return alert;
    }

    public async Task<Alert> AddAlert(Alert alert)
    {
        var user = await _context.Users.FindAsync(alert.UserId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with id: {alert.UserId} was not found.");
        }
        alert.CreatedAt = DateTime.UtcNow;
        await _context.Alerts.AddAsync(alert);
        await _context.SaveChangesAsync();
        return alert;
    }

    public async Task<Alert> UpdateAlert(Alert alert)
    {
        var alertdb = await _context.Alerts.FindAsync(alert.Id);
        if (alertdb == null)
        {
            throw new AlertNotFoundException($"Alert with id: {alert.Id} was not found.");
        }
        _mapper.Map(alert, alertdb);
        await _context.SaveChangesAsync();
        return alert;
    }

    public async Task DeleteAlert(Guid alertId)
    {
        var alert = await _context.Alerts.FindAsync(alertId);
        if (alert == null)
        {
            throw new AlertNotFoundException($"Alert with id: {alertId} was not found.");
        }

        _context.Alerts.Remove(alert);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsersByMatchingAlerts(string from, string to, DateTime? departureDate, decimal? price)
    {
        return await _context.Alerts
            .Where(a =>
                (string.IsNullOrEmpty(from) || a.FromAirport == from) &&
                (string.IsNullOrEmpty(to) || a.ToAirport == to) &&
                (!departureDate.HasValue || a.DepartureDate.Date == departureDate.Value.Date) &&
                (!price.HasValue || a.MaxPrice >= price) &&
                a.IsActive &&
                a.IsOn)
            .Include(a => a.User)
            .Select(a => a.User)
            .ToListAsync();
    }
}