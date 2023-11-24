using Application.Common;
using Application.EntityManagement.Orders.Queries;
using Application.EntityManagement.Products.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class OrderItemType : ObjectType<OrderItem>
{
    protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
    {
        descriptor.Description("Represents a single item of an order.");

        descriptor
            .Field(orderItem => orderItem.Order)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetOrderAsync(default!, default!))
            .Description("The Order This Item Belongs To");

        descriptor
            .Field(orderItem => orderItem.Product)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetProductAsync(default!, default!))
            .Description("The Product This Item Represents");

        descriptor
            .Field(orderItem => orderItem.Quantity)
            .Description("The Total Number of Product Units This Item Represents");

        descriptor
            .Field(orderItem => orderItem.UnitPrice)
            .Description("The Price of a Single Unit of The Product This Item Represents");

        descriptor
            .Field(orderItem => orderItem.SubTotalPrice)
            .Description("The Total Price of This Item");

        descriptor
            .Field(orderItem => orderItem.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(orderItem => orderItem.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(orderItem => orderItem.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(orderItem => orderItem.InternalId)
            .Ignore();

        descriptor
            .Field(orderItem => orderItem.OrderId)
            .Ignore();

        descriptor
            .Field(orderItem => orderItem.ProductId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<Product?> GetProductAsync([Parent] OrderItem orderItem, [Service] ISender sender)
        {
            var productsQuery = new GetAllProductsQuery(
                new Pagination(),
                null,
                x => x.InternalId == orderItem.ProductId);

            var result = await sender.Send(productsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<Order?> GetOrderAsync([Parent] OrderItem orderItem, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(
                new Pagination(),
                null,
                x => x.InternalId == orderItem.OrderId);

            var result = await sender.Send(ordersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}