namespace Region.Persistence.Infrastructure.Queue;
public static class RabbitMqConstants
{
    public const string RegionPersistenceExchange = $"region.persistence.exchange";

    public const string RegisterRegionQueueName = "region.persistence.on-register-region";
    public const string RegisterRegionRoutingKey = "on-register-region";
}
