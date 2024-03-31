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
            .Description("Represents a user with details such as username, email, personal information, addresses, phone numbers, orders, and associated roles.");

        descriptor
            .Field(user => user.Cart)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetCartAsync(default!, default!))
            .Description("The Cart Associated with the User\n" +
                         "Authentication is required.");

        descriptor
            .Field(user => user.Person)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetPersonAsync(default!, default!))
            .Description("The Personal Information of the User\n" +
                         "Authentication is required.");

        descriptor
            .Field(user => user.Addresses)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetAddressesAsync(default!, default!))
            
            .Description("The Addresses Associated with the User\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for address details.");

        descriptor
            .Field(user => user.Answers)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetAnswersAsync(default!, default!))
            
            .Description("The Answers Posted by the User\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for answer details.");

        descriptor
            .Field(user => user.Comments)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetCommentsAsync(default!, default!))
            
            .Description("The Comments Posted by the User\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for comment details.");

        descriptor
            .Field(user => user.Orders)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetOrdersAsync(default!, default!))
            
            .Description("The Orders Associated with the User\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for order details.");

        descriptor
            .Field(user => user.UserRoles)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserRolesAsync(default!, default!))
            .Description("The User-Roles Associated with the User\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for user-role details.");

        descriptor
            .Field(user => user.Questions)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetQuestionsAsync(default!, default!))
            
            .Description("The Questions Posted by the User\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for question details.");

        descriptor
            .Field(user => user.Votes)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetVotesAsync(default!, default!))
            .Description("The Votes Associated with the User\n" +
                         "Authentication is required.");

        descriptor
            .Field(user => user.PhoneNumbers)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetPhoneNumbersAsync(default!, default!))
            
            .Description("The Phone Numbers Associated with the User\n" +
                         "Authentication is required.\n" +
                         "Supports sorting for phone number details.");

        descriptor
            .Field(user => user.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(user => user.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(user => user.ExternalId)
            .Description("The External ID for Client Interactions")
            ;

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
        public static async Task<Person?> GetPersonAsync([Parent] User user, [Service] ISender sender)
        {
            var personsQuery = new GetAllPersonsQuery(
                new Pagination(),
                x => x.InternalId == user.PersonId);

            var result = await sender.Send(personsQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<Cart?> GetCartAsync([Parent] User user, [Service] ISender sender)
        {
            var cartsQuery = new GetAllCartsQuery(
                new Pagination(),
                x => x.InternalId == user.CartId);

            var result = await sender.Send(cartsQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<IEnumerable<Address>?> GetAddressesAsync([Parent] User user, [Service] ISender sender)
        {
            var addressesQuery = new GetAllAddressesQuery(
                Pagination.MaxPagination,
                address => address.UserId == user.InternalId);

            var result = await sender.Send(addressesQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Answer>?> GetAnswersAsync([Parent] User user, [Service] ISender sender)
        {
            var answersQuery = new GetAllAnswersQuery(
                Pagination.MaxPagination,
                answer => answer.UserId == user.InternalId);

            var result = await sender.Send(answersQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Comment>?> GetCommentsAsync([Parent] User user, [Service] ISender sender)
        {
            var commentsQuery = new GetAllCommentsQuery(
                Pagination.MaxPagination,
                comment => comment.UserId == user.InternalId);

            var result = await sender.Send(commentsQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Order>?> GetOrdersAsync([Parent] User user, [Service] ISender sender)
        {
            var ordersQuery = new GetAllOrdersQuery(
                Pagination.MaxPagination,
                order => order.UserId == user.InternalId);

            var result = await sender.Send(ordersQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Question>?> GetQuestionsAsync([Parent] User user, [Service] ISender sender)
        {
            var questionsQuery = new GetAllQuestionsQuery(
                Pagination.MaxPagination,
                question => question.UserId == user.InternalId);

            var result = await sender.Send(questionsQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<UserRole>?> GetUserRolesAsync([Parent] User user, [Service] ISender sender)
        {
            var userRolesQuery = new GetAllUserRolesQuery(
                Pagination.MaxPagination,
                userRole => userRole.UserId == user.InternalId);

            var result = await sender.Send(userRolesQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<Vote>?> GetVotesAsync([Parent] User user, [Service] ISender sender)
        {
            var votesQuery = new GetAllVotesQuery(
                Pagination.MaxPagination,
                vote => vote.UserId == user.InternalId);

            var result = await sender.Send(votesQuery);

            return result.Data;
        }

        public static async Task<IEnumerable<PhoneNumber>?> GetPhoneNumbersAsync([Parent] User user, [Service] ISender sender)
        {
            var phoneNumbersQuery = new GetAllPhoneNumbersQuery(
                Pagination.MaxPagination,
                phoneNumber => phoneNumber.UserId == user.InternalId);

            var result = await sender.Send(phoneNumbersQuery);

            return result.Data;
        }
    }
}