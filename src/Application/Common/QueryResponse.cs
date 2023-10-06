using System.Net;

namespace Application.Common;

public record QueryResponse
(
    object? Data = null,
    bool IsSuccessful = false,
    string Message = "",
    HttpStatusCode HttpStatusCode = HttpStatusCode.BadRequest
);