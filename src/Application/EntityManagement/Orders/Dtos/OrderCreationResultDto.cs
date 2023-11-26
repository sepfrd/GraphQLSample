using System.Net;

namespace Application.EntityManagement.Orders.Dtos;

public record OrderCreationResultDto(object? Data, HttpStatusCode HttpStatusCode);