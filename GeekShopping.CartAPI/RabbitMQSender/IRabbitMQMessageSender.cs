using GeekShopping.MessageBus;

namespace GeekShopping.CartAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void Send(BaseMessage baseMessage, string queueName);
    }
}
