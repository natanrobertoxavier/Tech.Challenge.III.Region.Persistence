using RabbitMq.Package.Events;
using Region.Persistence.Infrastructure.Queue;

namespace Region.Persistence.Application.Events;
public class OnRegionCreateEvent : IRabbitMqEvent<OnRegionCreateMessage>
{
    public string Exchange => RabbitMqConstants.UserPersistenceExchange;

    public string RoutingKey => RabbitMqConstants.RegisterUserRoutingKey;

    public OnRegionCreateMessage Payload { get; set; }
}
