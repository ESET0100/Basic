using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory() { HostName = "localhost", VirtualHost = "smart-dev", UserName = "smart-proj", Password = "1234" };

string _exchangeName = "order.direct";
string queueName = "q.create_order";
string routingKey = "order.created";

using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: _exchangeName, type: ExchangeType.Direct);
await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false);

// Bind queue to exchange with routing key
await channel.QueueBindAsync(queue: queueName, exchange: _exchangeName, routingKey: routingKey);

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[Direct Exchange {queueName}] Received: {message}");
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();