namespace Domain.Abstractions;

public interface IHasUuid
{
    Guid Uuid { get; init; }
}