namespace Region.Persistence.Infrastructure.Queue;
public static class RabbitMqConstants
{
    public const string UserPersistenceExchange = $"region.persistence.exchange";

    public const string RegisterUserQueueName = "region.persistence.on-register-region";
    public const string RegisterUserRoutingKey = "on-register-region";
}
