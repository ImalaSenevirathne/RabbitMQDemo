using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMq.ProductAPI.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public async void SendProductMessage<T>(T message)
        {
            // Here we specify the Rabbit MQ Server. we use rabbitmq docker image for this purpose
            var factory = new ConnectionFactory { HostName = "localhost" };

            // Create a connection to the RabbitMQ server using connection factory details as mentioned above
            var connection = await factory.CreateConnectionAsync();

            // Create a channel using the connection
            using var channel = await connection.CreateChannelAsync();

            // Declare the queue after mentioning name and a few property related to that
            await channel.QueueDeclareAsync("productqueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            // Serialize the message to a byte array using JSON serialization
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            //put the data on the product queue
            await channel.BasicPublishAsync(exchange: "", routingKey: "productqueue", body: body);
        }
    }
}
