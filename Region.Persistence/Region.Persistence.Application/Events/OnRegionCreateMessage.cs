using Region.Persistence.Domain.Entities;

namespace Region.Persistence.Application.Events;
public class OnRegionCreateMessage(
    Guid id,
    DateTime registrationDate,
    int dDD,
    string region,
    Guid userId)
{
    public Guid Id { get; set; } = id;
    public DateTime RegistrationDate { get; set; } = registrationDate;
    public int DDD { get; set; } = dDD;
    public string Region { get; set; } = region;
    public Guid UserId { get; set; } = userId;
}
