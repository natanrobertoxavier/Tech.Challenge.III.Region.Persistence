using MediatR;
using Region.Persistence.Application.Interfaces;
using Region.Persistence.Domain.Messages.DomaiEvents;
using Serilog;

namespace Region.Persistence.Application.Messages.Handlers;
public class RegionEventHandler(
    ILogger logger,
    IEventAppService eventAppService) : INotificationHandler<RegionCreateDomainEvent>
{
    private readonly ILogger _logger = logger;
    private readonly IEventAppService _eventAppService = eventAppService;

    public async Task Handle(RegionCreateDomainEvent message, CancellationToken cancellationToken)
    {
        await _eventAppService.SendRegionCreateEvent(message);
    }
}
