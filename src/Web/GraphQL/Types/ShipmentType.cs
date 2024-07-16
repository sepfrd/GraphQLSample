using Application.EntityManagement.Addresses.Queries;
using Application.EntityManagement.Orders.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class ShipmentType : ObjectType<Shipment>
{
    protected override void Configure(IObjectTypeDescriptor<Shipment> descriptor)
    {
        descriptor
            .Description(
                "Represents a shipment with details like status, shipping method, dates, cost, and associated addresses.");

        descriptor
            .Field(shipment => shipment.Order)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetOrderAsync(default!, default!))
            .Description("The Order Associated with the Shipment\n" +
                         "Authentication is required.");

        descriptor
            .Field(shipment => shipment.DestinationAddressId)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetDestinationAddressAsync(default!, default!))
            .Description("The Destination Address Associated with the Shipment\n" +
                         "Authentication is required.");

        descriptor
            .Field(shipment => shipment.OriginAddressId)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetOriginAddressAsync(default!, default!))
            .Description("The Origin Address Associated with the Shipment\n" +
                         "Authentication is required.");

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
        public async static Task<Order?> GetOrderAsync([Parent] Shipment shipment, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(x => x.InternalId == shipment.OrderId);

            var result = await sender.Send(ordersQuery);

            return result.Data?.FirstOrDefault();
        }

        public async static Task<Address?> GetDestinationAddressAsync([Parent] Shipment shipment,
            [Service] ISender sender)
        {
            var addressesQuery = new GetAllAddressesQuery(x => x.InternalId == shipment.DestinationAddressId);

            var result = await sender.Send(addressesQuery);

            return result.Data?.FirstOrDefault();
        }

        public async static Task<Address?> GetOriginAddressAsync([Parent] Shipment shipment, [Service] ISender sender)
        {
            var addressesQuery = new GetAllAddressesQuery(x => x.InternalId == shipment.OriginAddressId);

            var result = await sender.Send(addressesQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}