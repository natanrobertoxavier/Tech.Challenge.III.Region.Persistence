using Region.Persistence.Application.Events;
using Region.Persistence.Application.Interfaces;
using Region.Persistence.Domain.Messages.DomaiEvents;
using Region.Persistence.Infrastructure.Queue;

namespace Region.Persistence.Application.Services;
public class EventsAppService(
    IRabbitMqEventsDispatcher rabbitMqEventsDispatcher) : IEventAppService
{
    private readonly IRabbitMqEventsDispatcher _rabbitMqEventsDispatcher = rabbitMqEventsDispatcher;

    public async Task SendRegionCreateEvent(RegionCreateDomainEvent message)
    {
        await _rabbitMqEventsDispatcher.SendEvent(new OnRegionCreateEvent()
        {
            Payload = new OnRegionCreateMessage(
                message.Id,
                message.RegistrationDate,
                message.DDD,
                message.Region,
                message.UserId)
        });
    }
}
