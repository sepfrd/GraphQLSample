using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Payment : BaseEntity
{
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}