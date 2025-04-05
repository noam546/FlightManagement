namespace UserAlertManagement.Services.Interfaces;

public interface IMessageQueue
{
    Task PublishAsync<T>(string queueName, T message);
    void Subscribe<T>(string queueName, Func<T, Task> onMessage);
}