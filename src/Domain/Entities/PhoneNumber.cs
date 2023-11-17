using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class PhoneNumber : BaseEntity
{
    public PhoneNumber(string number, PhoneNumberType type)
    {
        Number = number;
        Type = type;
    }

    public string Number { get; set; }

    public PhoneNumberType Type { get; set; }

    public bool IsConfirmed { get; set; }

    public User? User { get; set; }

    public Guid UserId { get; set; }
}