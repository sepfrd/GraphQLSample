#region

using Application.EntityManagement.OrderItems.Queries;
using Application.EntityManagement.Payments.Queries;
using Application.EntityManagement.Shipments.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

#endregion

namespace Web.GraphQL.Types;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor
            .Field(order => order.Payment)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetPaymentAsync(default!, default!));

        descriptor
            .Field(order => order.Shipment)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetShipmentAsync(default!, default!));

        descriptor
            .Field(order => order.OrderItems)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetOrderItemsAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(order => order.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(order => order.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(order => order.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(order => order.InternalId)
            .Ignore();

        descriptor
            .Field(order => order.PaymentId)
            .Ignore();

        descriptor
            .Field(order => order.ShipmentId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<Payment?> GetPaymentAsync([Parent] Order order, [Service] ISender sender)
        {
            var paymentsQuery = new GetAllPaymentsQuery(
                new Pagination(),
                x => x.OrderId == order.InternalId);

            var result = await sender.Send(paymentsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<Shipment?> GetShipmentAsync([Parent] Order order, [Service] ISender sender)
        {
            var shipmentsQuery = new GetAllShipmentsQuery(
                new Pagination(),
                x => x.OrderId == order.InternalId);

            var result = await sender.Send(shipmentsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderItem>?> GetOrderItemsAsync([Parent] Order order, [Service] ISender sender)
        {
            var orderItemsQuery = new GetAllOrderItemsQuery(
                new Pagination(),
                x => x.OrderId == order.InternalId);

            var result = await sender.Send(orderItemsQuery);

            return result.Data;
        }
    }
}