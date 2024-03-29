using Domain.Entities;

namespace IntegrationTests.Dtos;

public record ProductsQueryResponse(List<Product>? Products);