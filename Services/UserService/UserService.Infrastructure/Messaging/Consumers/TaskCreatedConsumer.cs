using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using SharedKernel.Events;
using System.Text.Json;
using Polly.Retry;
using Polly;

namespace UserService.Infrastructure.Messaging.Consumers
{
    public class TaskCreatedConsumer : BackgroundService, IAsyncDisposable
    {
        private IConnection _connection;
        private IChannel _channel;
        private readonly AsyncRetryPolicy _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5),
            (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Retry {retryCount} after {timeSpan.TotalSeconds}s due to: {exception.Message}");
            });

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "rabbitmq",
                    UserName = "guest",
                    Password = "guest"
                };

                _connection = await factory.CreateConnectionAsync(cancellationToken);
                _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
                await _channel.QueueDeclareAsync(queue: "task_created", durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);

                await base.StartAsync(cancellationToken);
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await HandleMessageAsync(JsonSerializer.Deserialize<TaskCreated?>(message));
                await _channel.BasicAckAsync(ea.DeliveryTag, false, cancellationToken: stoppingToken);
            };

            await _channel.BasicConsumeAsync(queue: "task_created", autoAck: false, consumer: consumer, stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private Task HandleMessageAsync(TaskCreated? message)
        {
            // TODO: Replace with proper logging to a database
            Console.WriteLine($"Received: {message?.Title}");
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
                await _channel.CloseAsync();

            if (_connection != null)
                await _connection.CloseAsync();

            base.Dispose();
        }
    }
}
