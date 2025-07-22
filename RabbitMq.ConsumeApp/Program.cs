using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Here we specify the RabbitMQ server. We use the RabbitMQ Docker image for this purpose.
var factory = new ConnectionFactory { HostName = "localhost" };

// Create a connection to the RabbitMQ server using connection factory details as mentioned above
var connection = await factory.CreateConnectionAsync();

// Create a channel using the connection
using var channel = await connection.CreateChannelAsync();

// Declare the queue after mentioning name and a few properties related to that
await channel.QueueDeclareAsync("productqueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

// Create a consumer to listen to the queue
var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    // Here you can process the message as needed
    Console.WriteLine($"Received message: {message}");
};

//read the message
channel.BasicConsumeAsync(queue: "productqueue", autoAck: true, consumer: consumer);
Console.ReadKey();
