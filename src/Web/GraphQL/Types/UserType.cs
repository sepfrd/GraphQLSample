using Application.Common;
using Application.EntityManagement.Addresses.Queries;
using Application.EntityManagement.Answers.Queries;
using Application.EntityManagement.Carts.Queries;
using Application.EntityManagement.Comments.Queries;
using Application.EntityManagement.Orders.Queries;
using Application.EntityManagement.Payments.Queries;
using Application.EntityManagement.Persons.Queries;
using Application.EntityManagement.PhoneNumbers.Queries;
using Application.EntityManagement.Questions.Queries;
using Application.EntityManagement.UserRoles.Queries;
using Application.EntityManagement.Votes.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .Field(user => user.Cart)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetCartAsync(default!, default!));

        descriptor
            .Field(user => user.Person)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetPersonAsync(default!, default!));
        
        descriptor
            .Field(user => user.Addresses)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetAddressesAsync(default!, default!));
        
        descriptor
            .Field(user => user.Answers)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetAnswersAsync(default!, default!));
        
        descriptor
            .Field(user => user.Comments)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetCommentsAsync(default!, default!));
        
        descriptor
            .Field(user => user.Orders)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetOrdersAsync(default!, default!));
        
        descriptor
            .Field(user => user.UserRoles)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserRolesAsync(default!, default!));
        
        descriptor
            .Field(user => user.Payments)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetPaymentsAsync(default!, default!));
        
        descriptor
            .Field(user => user.Questions)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetQuestionsAsync(default!, default!));
        
        descriptor
            .Field(user => user.Votes)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetVotesAsync(default!, default!));
        
        descriptor
            .Field(user => user.PhoneNumbers)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetPhoneNumbersAsync(default!, default!));
        
        descriptor
            .Field(user => user.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(user => user.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(user => user.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(user => user.InternalId)
            .Ignore();

        descriptor
            .Field(user => user.CartId)
            .Ignore();

        descriptor
            .Field(user => user.PersonId)
            .Ignore();
    }

    private class Resolvers
    {
        public static async Task<Person?> GetPersonAsync([Parent] User user, [Service] ISender sender)
        {
            var personsQuery = new GetAllPersonsQuery(
                new Pagination(),
                null,
                x => x.InternalId == user.PersonId);

            var result = await sender.Send(personsQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<Cart?> GetCartAsync([Parent] User user, [Service] ISender sender)
        {
            var cartsQuery = new GetAllCartsQuery(
                new Pagination(),
                null,
                x => x.InternalId == user.CartId);

            var result = await sender.Send(cartsQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<IEnumerable<Address>?> GetAddressesAsync([Parent] User user, [Service] ISender sender)
        {
            var addressesQuery = new GetAllAddressesQuery(
                new Pagination(1, int.MaxValue),
                null,
                address => address.UserId == user.InternalId);

            var result = await sender.Send(addressesQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Answer>?> GetAnswersAsync([Parent] User user, [Service] ISender sender)
        {
            var answersQuery = new GetAllAnswersQuery(
                new Pagination(1, int.MaxValue),
                null,
                answer => answer.UserId == user.InternalId);

            var result = await sender.Send(answersQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Comment>?> GetCommentsAsync([Parent] User user, [Service] ISender sender)
        {
            var commentsQuery = new GetAllCommentsQuery(
                new Pagination(1, int.MaxValue),
                null,
                comment => comment.UserId == user.InternalId);

            var result = await sender.Send(commentsQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Order>?> GetOrdersAsync([Parent] User user, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(
                new Pagination(1, int.MaxValue),
                null,
                order => order.UserId == user.InternalId);

            var result = await sender.Send(ordersQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Payment>?> GetPaymentsAsync([Parent] User user, [Service] ISender sender)
        {
            var paymentsQuery = new GetAllPaymentsQuery(
                new Pagination(1, int.MaxValue),
                null,
                payment => payment.UserId == user.InternalId);

            var result = await sender.Send(paymentsQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Question>?> GetQuestionsAsync([Parent] User user, [Service] ISender sender)
        {
            var questionsQuery = new GetAllQuestionsQuery(
                new Pagination(1, int.MaxValue),
                null,
                question => question.UserId == user.InternalId);

            var result = await sender.Send(questionsQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<UserRole>?> GetUserRolesAsync([Parent] User user, [Service] ISender sender)
        {
            var userRolesQuery = new GetAllUserRolesQuery(
                new Pagination(1, int.MaxValue),
                null,
                userRole => userRole.UserId == user.InternalId);

            var result = await sender.Send(userRolesQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Vote>?> GetVotesAsync([Parent] User user, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(
                new Pagination(1, int.MaxValue),
                null,
                vote => vote.UserId == user.InternalId);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<PhoneNumber>?> GetPhoneNumbersAsync([Parent] User user, [Service] ISender sender)
        {
            var phoneNumbersQuery = new GetAllPhoneNumbersQuery(
                new Pagination(1, int.MaxValue),
                null,
                phoneNumber => phoneNumber.UserId == user.InternalId);

            var result = await sender.Send(phoneNumbersQuery);

            return result.Data;
        }
    }
}