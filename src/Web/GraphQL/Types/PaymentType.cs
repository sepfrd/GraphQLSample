using Application.EntityManagement.Orders.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class PaymentType : ObjectType<Payment>
{
    protected override void Configure(IObjectTypeDescriptor<Payment> descriptor)
    {
        descriptor
            .Description("Represents a payment associated with an order, including details like the payment amount, method, and status.");

        descriptor
            .Field(payment => payment.Order)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetOrderAsync(default!, default!))
            .Description("The Order Associated with the Payment\n" +
                         "Authentication is required.");

        descriptor
            .Field(payment => payment.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(payment => payment.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(payment => payment.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(payment => payment.InternalId)
            .Ignore();

        descriptor
            .Field(payment => payment.OrderId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public static async Task<Order?> GetOrderAsync([Parent] Payment payment, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(
                new Pagination(),
                x => x.InternalId == payment.OrderId);

            var result = await sender.Send(ordersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}