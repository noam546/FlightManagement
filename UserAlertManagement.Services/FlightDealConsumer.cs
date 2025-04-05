using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserAlertManagement.Services.Interfaces;
using UserAlertManagement.Services.Models;

namespace UserAlertManagement.Services;

public class FlightDealConsumer : IHostedService
{
    private readonly IMessageQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;

    public FlightDealConsumer(IMessageQueue queue, IServiceScopeFactory scopeFactory)
    {
        _queue = queue;
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _queue.Subscribe<FlightDealEvent>("flight-deals", HandleFlightDealAsync);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task HandleFlightDealAsync(FlightDealEvent flightEvent)
    {
        using var scope = _scopeFactory.CreateScope();
        var userAlertService = scope.ServiceProvider.GetRequiredService<IUserAlertService>();

        var matchingUsers = await userAlertService.GetUsersMatchingAlerts(
            flightEvent.From, flightEvent.To, flightEvent.DepartureDate, flightEvent.Price);

        if (!matchingUsers.Any()) return;

        var usersToNotify = matchingUsers.Select(user => new UserNotificationDto
        {
            UserId = user.Id,
            DeviceToken = user.DeviceToken
        }).ToList();

        var notificationEvent = new UsersToNotifyEvent
        {
            FlightId = flightEvent.FlightId,
            Message = $"Deal: {flightEvent.From} → {flightEvent.To} for {flightEvent.Price:C} on {flightEvent.DepartureDate:MMM dd}!",
            Users = usersToNotify
        };

        await _queue.PublishAsync("users-to-notify", notificationEvent);
    }
}