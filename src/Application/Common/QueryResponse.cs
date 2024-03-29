using System.Net;

namespace Application.Common;

public record QueryResponse<T>(
    T? Data = null,
    bool IsSuccessful = false,
    string Message = "",
    HttpStatusCode HttpStatusCode = HttpStatusCode.BadRequest)
    where T : class;