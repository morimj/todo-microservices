using RabbitMQ.Client;
using SharedKernel.Events;
using System.Text;
using System.Text.Json;
using TaskService.Application.Publishers;

namespace TaskService.Infrastructure.Messaging.Publishers
{
    public class RabbitMqTaskCreatedPublisher : ITaskCreatedPublisher, IAsyncDisposable
    {
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly ConnectionFactory _factory;
        private readonly SemaphoreSlim _connectionLock = new(1, 1);

        public RabbitMqTaskCreatedPublisher()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest"
            };
        }

        public async Task InitializeAsync()
        {
            await _connectionLock.WaitAsync();

            try
            {
                if (_connection == null)
                {
                    _connection = await _factory.CreateConnectionAsync();
                    _channel = await _connection.CreateChannelAsync();

                    await _channel.QueueDeclareAsync("task_created", durable: true, exclusive: false, autoDelete: false);
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        public async Task PublishAsync(Guid taskId, string title)
        {
            if (_connection == null || _channel == null)
                await InitializeAsync();

            var taskCreated = new TaskCreated(taskId, title, DateTime.UtcNow, Guid.NewGuid());
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(taskCreated));

            await _channel.BasicPublishAsync(exchange: "", routingKey: "task_created", body: body);
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
            {
                await _channel.CloseAsync();
                _channel.Dispose();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync();
                _connection.Dispose();
            }
        }
    }
}
