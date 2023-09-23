using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class PhoneNumber : BaseEntity
{
    public PhoneNumber(string number, PhoneNumberType type)
    {
        Number = number;
        Type = type;
    }
    
    public string Number { get; set; }
    public PhoneNumberType Type { get; set; }
}

