using Domain.Entities;

namespace IntegrationTests.Dtos;

public record CategoriesQueryResponse(List<Category>? Categories);