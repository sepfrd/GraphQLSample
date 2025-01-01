using Application.EntityManagement.Orders.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class PaymentType : ObjectType<Payment>
{
    protected override void Configure(IObjectTypeDescriptor<Payment> descriptor)
    {
        descriptor
            .Description(
                "Represents a payment associated with an order, including details like the payment amount, method, and status.");

        descriptor
            .Field(payment => payment.Order)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetOrderAsync(null!, null!))
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
        public async static Task<Order?> GetOrderAsync([Parent] Payment payment, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(x => x.InternalId == payment.OrderId);

            var result = await sender.Send(ordersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}