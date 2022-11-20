using GeekShopping.CartAPI.Messages;
using GeekShopping.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.CartAPI.RabbitMQSender
{
    public class RabbitMQMesssageSender : IRabbitMQMessageSender
    {

        private readonly IConfiguration _configuration;

        public RabbitMQMesssageSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(BaseMessage message, string queueName)
        {
            var host = _configuration["MessageBroker:host"];
            var user = _configuration["MessageBroker:user"];
            var password = _configuration["MessageBroker:password"];

            var connectionFactory = new ConnectionFactory
            {
                HostName = host,
                UserName = user,
                Password = password,
            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, autoDelete: false, exclusive: false);
            byte[] body = GetMessageAsByteArray(message);
            channel.BasicPublish(
                            exchange: "",
                            routingKey: queueName,
                            basicProperties: null,
                            body: body
                        );

        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<CheckoutHeaderVO>((CheckoutHeaderVO) message, options);

            return Encoding.UTF8.GetBytes(json);

        }
    }
}
