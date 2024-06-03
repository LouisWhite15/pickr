using System.Text.Json.Serialization;

namespace Selections.Domain.Aggregates.SelectionAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SelectionStatus
{
    Unknown = 0,
    Started = 1,
    Submitted = 1,
    AwaitingValidation = 2,
    StockConfirmed = 3,
    Cancelled = 4,
}