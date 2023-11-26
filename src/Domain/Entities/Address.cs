#region

using Domain.Common;

#endregion

namespace Domain.Entities;

public sealed class Address : BaseEntity
{
    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? UnitNumber { get; set; }

    public string? BuildingNumber { get; set; }

    public User? User { get; set; }

    public Guid UserId { get; set; }
}