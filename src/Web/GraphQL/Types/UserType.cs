using Application.EntityManagement.Addresses.Queries;
using Application.EntityManagement.Answers.Queries;
using Application.EntityManagement.Carts.Queries;
using Application.EntityManagement.Comments.Queries;
using Application.EntityManagement.Orders.Queries;
using Application.EntityManagement.Persons.Queries;
using Application.EntityManagement.PhoneNumbers.Queries;
using Application.EntityManagement.Questions.Queries;
using Application.EntityManagement.UserRoles.Queries;
using Application.EntityManagement.Votes.Queries;
using Domain.Common;
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
                    resolvers.GetCartAsync(default!, default!));

        descriptor
            .Field(user => user.Person)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetPersonAsync(default!, default!));

        descriptor
            .Field(user => user.Addresses)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetAddressesAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(user => user.Answers)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetAnswersAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(user => user.Comments)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetCommentsAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(user => user.Orders)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetOrdersAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(user => user.UserRoles)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetUserRolesAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(user => user.Questions)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetQuestionsAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(user => user.Votes)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetVotesAsync(default!, default!));

        descriptor
            .Field(user => user.PhoneNumbers)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetPhoneNumbersAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(user => user.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(user => user.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(user => user.ExternalId)
            .Description("The External ID for Client Interactions")
            .UseFiltering();

        descriptor
            .Field(user => user.InternalId)
            .Ignore();

        descriptor
            .Field(user => user.Password)
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
        public async Task<Person?> GetPersonAsync([Parent] User user, [Service] ISender sender)
        {
            var personsQuery = new GetAllPersonsQuery(
                new Pagination(),
                x => x.InternalId == user.PersonId);

            var result = await sender.Send(personsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<Cart?> GetCartAsync([Parent] User user, [Service] ISender sender)
        {
            var cartsQuery = new GetAllCartsQuery(
                new Pagination(),
                x => x.InternalId == user.CartId);

            var result = await sender.Send(cartsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<IEnumerable<Address>?> GetAddressesAsync([Parent] User user, [Service] ISender sender)
        {
            var addressesQuery = new GetAllAddressesQuery(
                Pagination.MaxPagination,
                address => address.UserId == user.InternalId);

            var result = await sender.Send(addressesQuery);

            return result.Data;
        }

        public async Task<IEnumerable<Answer>?> GetAnswersAsync([Parent] User user, [Service] ISender sender)
        {
            var answersQuery = new GetAllAnswersQuery(
                Pagination.MaxPagination,
                answer => answer.UserId == user.InternalId);

            var result = await sender.Send(answersQuery);

            return result.Data;
        }

        public async Task<IEnumerable<Comment>?> GetCommentsAsync([Parent] User user, [Service] ISender sender)
        {
            var commentsQuery = new GetAllCommentsQuery(
                Pagination.MaxPagination,
                comment => comment.UserId == user.InternalId);

            var result = await sender.Send(commentsQuery);

            return result.Data;
        }

        public async Task<IEnumerable<Order>?> GetOrdersAsync([Parent] User user, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(
                Pagination.MaxPagination,
                order => order.UserId == user.InternalId);

            var result = await sender.Send(ordersQuery);

            return result.Data;
        }

        public async Task<IEnumerable<Question>?> GetQuestionsAsync([Parent] User user, [Service] ISender sender)
        {
            var questionsQuery = new GetAllQuestionsQuery(
                Pagination.MaxPagination,
                question => question.UserId == user.InternalId);

            var result = await sender.Send(questionsQuery);

            return result.Data;
        }

        public async Task<IEnumerable<UserRole>?> GetUserRolesAsync([Parent] User user, [Service] ISender sender)
        {
            var userRolesQuery = new GetAllUserRolesQuery(
                Pagination.MaxPagination,
                userRole => userRole.UserId == user.InternalId);

            var result = await sender.Send(userRolesQuery);

            return result.Data;
        }

        public async Task<IEnumerable<Vote>?> GetVotesAsync([Parent] User user, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(
                Pagination.MaxPagination,
                vote => vote.UserId == user.InternalId);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }

        public async Task<IEnumerable<PhoneNumber>?> GetPhoneNumbersAsync([Parent] User user, [Service] ISender sender)
        {
            var phoneNumbersQuery = new GetAllPhoneNumbersQuery(
                Pagination.MaxPagination,
                phoneNumber => phoneNumber.UserId == user.InternalId);

            var result = await sender.Send(phoneNumbersQuery);

            return result.Data;
        }
    }
}