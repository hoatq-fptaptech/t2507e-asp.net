using System.Text;
using RabbitMQ.Client;

namespace T2507E_ASP.Messaging;

public class RabbitMqPublisher
{
    private const string QueueName = "demo-queue";

    public async Task PublishAsync()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete:false
            );
        var msg = "Hello World!";
        var body = Encoding.UTF8.GetBytes(msg);
        await channel.BasicPublishAsync(
               exchange: "",
               routingKey: QueueName,
               body: body
            );
    }
}