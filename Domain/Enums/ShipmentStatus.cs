namespace Domain.Enums;

public enum ShipmentStatus
{
    Pending = 0,            // Shipment has not yet been processed.
    InTransit = 1,          // Shipment is en route to the destination.
    OutForDelivery = 2,     // Shipment is out for delivery to the recipient.
    Delivered = 3,          // Shipment has been successfully delivered.
    Delayed = 4,            // Shipment is delayed for some reason.
    Returned = 5,           // Shipment was returned to the sender.
    Lost = 6,               // Shipment is lost in transit.
    Damaged = 7,            // Shipment was damaged during transit.
    OnHold = 8,             // Shipment is on hold, not being processed.
    Canceled = 9            // Shipment has been canceled.
}