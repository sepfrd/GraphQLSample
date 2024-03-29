using Application.EntityManagement.OrderItems.Queries;
using Application.EntityManagement.Payments.Queries;
using Application.EntityManagement.Shipments.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor
            .Description("Represents a user's order, containing a list of order items, associated user information, and order status.");

        descriptor
            .Field(order => order.Payment)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetPaymentAsync(default!, default!))
            .Description("The Payment Information Associated with the Order\n" +
                         "Requires the order ID.\n" +
                         "Authentication is required.");

        descriptor
            .Field(order => order.Shipment)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetShipmentAsync(default!, default!))
            .Description("The Shipment Details Associated with the Order\n" +
                         "Requires the order ID.\n" +
                         "Authentication is required.");

        descriptor
            .Field(order => order.OrderItems)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetOrderItemsAsync(default!, default!))
            .UseFiltering()
            .UseSorting()
            .Description("The List of Order Items Associated with the Order\n" +
                         "Requires the order ID.\n" +
                         "Authentication is required.\n" +
                         "Supports filtering and sorting for order item details.");

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
        public static async Task<Payment?> GetPaymentAsync([Parent] Order order, [Service] ISender sender)
        {
            var paymentsQuery = new GetAllPaymentsQuery(
                new Pagination(),
                x => x.OrderId == order.InternalId);

            var result = await sender.Send(paymentsQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<Shipment?> GetShipmentAsync([Parent] Order order, [Service] ISender sender)
        {
            var shipmentsQuery = new GetAllShipmentsQuery(
                new Pagination(),
                x => x.OrderId == order.InternalId);

            var result = await sender.Send(shipmentsQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<IEnumerable<OrderItem>?> GetOrderItemsAsync([Parent] Order order, [Service] ISender sender)
        {
            var orderItemsQuery = new GetAllOrderItemsQuery(
                new Pagination(),
                x => x.OrderId == order.InternalId);

            var result = await sender.Send(orderItemsQuery);

            return result.Data;
        }
    }
}