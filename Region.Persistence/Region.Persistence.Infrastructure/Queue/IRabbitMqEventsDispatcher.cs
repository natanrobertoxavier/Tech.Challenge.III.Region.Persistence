using RabbitMq.Package.Events;

namespace Region.Persistence.Infrastructure.Queue;
public interface IRabbitMqEventsDispatcher
{
    Task SendEvent<T>(IRabbitMqEvent<T> eventToSend);
}
