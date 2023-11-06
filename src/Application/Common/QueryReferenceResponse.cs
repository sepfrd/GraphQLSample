using System.Net;

namespace Application.Common;

public record QueryReferenceResponse<T>(
    T? Data = null,
    bool IsSuccessful = false,
    string Message = "",
    HttpStatusCode HttpStatusCode = HttpStatusCode.BadRequest) where T : class;