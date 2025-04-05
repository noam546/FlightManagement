using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserAlertManagement.Services.Interfaces;

namespace UserAlertManagement.Services
{
    public class MessageQueue : IMessageQueue
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageQueue()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task PublishAsync<T>(string queueName, T message)
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body
            );

            return Task.CompletedTask;
        }

        public void Subscribe<T>(string queueName, Func<T, Task> onMessage)
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<T>(json);

                if (message != null)
                {
                    await onMessage(message);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }
    }
}
