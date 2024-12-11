using Region.Persistence.Domain.Messages.DomaiEvents;

namespace Region.Persistence.Application.Interfaces;
public interface IEventAppService
{
    Task SendRegionCreateEvent(RegionCreateDomainEvent message);
}
