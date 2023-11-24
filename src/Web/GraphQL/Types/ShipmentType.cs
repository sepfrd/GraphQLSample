using Application.Common;
using Application.EntityManagement.Addresses.Queries;
using Application.EntityManagement.Orders.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class ShipmentType : ObjectType<Shipment>
{
    protected override void Configure(IObjectTypeDescriptor<Shipment> descriptor)
    {
        descriptor
            .Field(shipment => shipment.Order)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetOrderAsync(default!, default!));

        descriptor
            .Field(shipment => shipment.DestinationAddressId)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetDestinationAddressAsync(default!, default!));

        descriptor
            .Field(shipment => shipment.OriginAddressId)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetOriginAddressAsync(default!, default!));

        descriptor
            .Field(shipment => shipment.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(shipment => shipment.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(shipment => shipment.ExternalId)
            .Description("The External ID for Client Interactions");

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

    private sealed class Resolvers
    {
        public async Task<Order?> GetOrderAsync([Parent] Shipment shipment, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(
                new Pagination(),
                null,
                x => x.InternalId == shipment.OrderId);

            var result = await sender.Send(ordersQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<Address?> GetDestinationAddressAsync([Parent] Shipment shipment, [Service] ISender sender)
        {
            var addressesQuery = new GetAllAddressesQuery(
                new Pagination(),
                null,
                x => x.InternalId == shipment.DestinationAddressId);

            var result = await sender.Send(addressesQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<Address?> GetOriginAddressAsync([Parent] Shipment shipment, [Service] ISender sender)
        {
            var addressesQuery = new GetAllAddressesQuery(
                new Pagination(),
                null,
                x => x.InternalId == shipment.OriginAddressId);

            var result = await sender.Send(addressesQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}