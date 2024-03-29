using Application.Common.Constants;
using Domain.Common;
using FluentAssertions;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using IntegrationTests.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public class UserTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public UserTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateDefaultClient();
    }

    [Theory]
    [MemberData(nameof(GetUsersData))]
    public async Task Get_Users_Returns_Expected_Result(
        bool isAuthenticated,
        string? role,
        bool isSuccessful,
        Pagination pagination)
    {
        // Arrange

        var graphQlClient = new GraphQLHttpClient(
            new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("http://localhost/graphql")
            },
            new NewtonsoftJsonSerializer(),
            _httpClient);

        if (isAuthenticated)
        {
            graphQlClient.AuthenticateRequest(role!);
        }

        var graphQlRequest = new GraphQLRequest
        {
            Query = """
                    query Users($pagination: PaginationInput) {
                        users(pagination: $pagination) {
                            dateCreated
                            dateModified
                            externalId
                            username
                            email
                            isEmailConfirmed
                            score
                        }
                    }
                    """,
            Variables = new
            {
                pagination
            }
        };

        // Act

        var graphQlResponse = await graphQlClient.SendQueryAsync<UsersQueryResponse>(graphQlRequest);

        // Assert

        if (isSuccessful)
        {
            graphQlResponse.Data.Users.Should().NotBeNullOrEmpty();
            graphQlResponse.Data.Users!.Count.Should().Be(pagination.PageSize);
        }
        else
        {
            graphQlResponse.Errors.Should().NotBeNullOrEmpty();
            graphQlResponse.Errors!.First().Extensions!.First().Value.Should().Be("AUTH_NOT_AUTHORIZED");
        }
    }

    public static TheoryData<bool, string?, bool, Pagination> GetUsersData() =>
        new()
        {
            {
                false,
                null,
                false,
                new Pagination()
            },
            {
                true,
                RoleConstants.Customer,
                false,
                new Pagination()
            },
            {
                true,
                RoleConstants.Admin,
                true,
                new Pagination(1, 100)
            }
        };
}