using MediatR;
using Region.Persistence.Domain.Messages;

namespace Region.Persistence.Application.Messages;
public class MessagePublisher(IMediator mediatr) : IMessagePublisher
{
    private readonly IMediator _mediatr = mediatr;

    public async Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent
        => await _mediatr.Publish(domainEvent);
}
