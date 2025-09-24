using Producer.Exchanges;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost", VirtualHost = "smart-dev", UserName = "smart-proj", Password = "1234" };

DirectExchange directExchange = new DirectExchange(factory);

Console.WriteLine("Enter messages to send (type 'exit' to quit):");

while (true)
{
    string? input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) continue;
    if (input.ToLower() == "exit") break;

    await directExchange.SendMessage(input);


}
