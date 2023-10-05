namespace Domain.Enums;

public enum PaymentStatus
{
    Pending = 0, // Payment has not been initiated.
    AuthorizedPending = 1, // Payment authorization is pending.
    Authorized = 2, // Payment has been authorized but not captured.
    Captured = 3, // Payment has been successfully captured.
    PartiallyRefunded = 4, // Partial refund has been processed.
    Refunded = 5, // Payment has been refunded.
    Failed = 6, // Payment has failed.
    Chargeback = 7, // Payment has been charged back.
    Voided = 8, // Payment has been voided.
    Canceled = 9 // Payment has been canceled.
}