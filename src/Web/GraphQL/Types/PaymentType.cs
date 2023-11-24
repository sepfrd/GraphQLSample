using Application.Common;
using Application.EntityManagement.Orders.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class PaymentType : ObjectType<Payment>
{
    protected override void Configure(IObjectTypeDescriptor<Payment> descriptor)
    {
        descriptor
            .Field(payment => payment.Order)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetOrderAsync(default!, default!));

        descriptor
            .Field(payment => payment.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetUserAsync(default!, default!));

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

        descriptor
            .Field(payment => payment.UserId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<Order?> GetOrderAsync([Parent] Payment payment, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(
                new Pagination(),
                null,
                x => x.InternalId == payment.OrderId);

            var result = await sender.Send(ordersQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<User?> GetUserAsync([Parent] Payment payment, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(
                new Pagination(),
                null,
                x => x.InternalId == payment.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}