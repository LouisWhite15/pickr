using MediatR;
using Selections.Domain.Aggregates.SelectionAggregate;

namespace Selections.Domain.Events;

public record SelectionStartedDomainEvent(Selection Selection)
    : INotification;