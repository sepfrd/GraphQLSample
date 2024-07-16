using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class ShipmentSortType : SortInputType<Shipment>
{
    protected override void Configure(ISortInputTypeDescriptor<Shipment> descriptor)
    {
        descriptor
            .Field(shipment => shipment.InternalId)
            .Ignore();

        descriptor
            .Field(shipment => shipment.OrderId)
            .Ignore();

        descriptor
            .Field(shipment => shipment.DestinationAddressId)
            .Ignore();

        descriptor
            .Field(shipment => shipment.OriginAddressId)
            .Ignore();
    }
}