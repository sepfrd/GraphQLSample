using Domain.Common;

namespace Domain.Entities;

public sealed class Address : BaseEntity
{
    public string? Street { get; init; }

    public string? City { get; init; }

    public string? State { get; init; }

    public string? PostalCode { get; init; }

    public string? Country { get; init; }

    public string? UnitNumber { get; init; }

    public string? BuildingNumber { get; init; }

    public User? User { get; set; }

    public Guid UserId { get; set; }
}