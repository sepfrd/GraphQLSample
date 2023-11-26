#region

using System.Net;

#endregion

namespace Application.EntityManagement.Orders.Dtos;

public record OrderCreationResultDto(object? Data, HttpStatusCode HttpStatusCode);