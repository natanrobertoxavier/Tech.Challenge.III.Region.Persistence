using MediatR;

namespace Region.Persistence.Domain.Messages;
public abstract class DomainEvent : BaseMessage, INotification
{
    protected DomainEvent() { }
}
