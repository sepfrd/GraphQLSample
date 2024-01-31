using Domain.Entities;

namespace IntegrationTests.Dtos;

public record UsersQueryResponse(List<User>? Users);