namespace Region.Persistence.Domain.Messages.DomaiEvents;
public class RegionCreateDomainEvent(
    Guid id,
    int dDD,
    string region,
    Guid userId) : DomainEvent
{
    public Guid Id { get; set; } = id;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public int DDD { get; set; } = dDD;
    public string Region { get; set; } = region;
    public Guid UserId { get; set; } = userId;
}
