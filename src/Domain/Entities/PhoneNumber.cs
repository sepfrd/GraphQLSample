using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class PhoneNumber : BaseEntity
{
    public string? Number { get; init; }

    public PhoneNumberType Type { get; init; }

    public bool IsConfirmed { get; set; }

    public User? User { get; set; }

    public Guid UserId { get; set; }
}