using RabbitMq.Package.Events;
using Region.Persistence.Infrastructure.Queue;

namespace Region.Persistence.Application.Events;
public class OnRegionCreateEvent : IRabbitMqEvent<OnRegionCreateMessage>
{
    public string Exchange => RabbitMqConstants.RegionPersistenceExchange;

    public string RoutingKey => RabbitMqConstants.RegisterRegionRoutingKey;

    public OnRegionCreateMessage Payload { get; set; }
}
