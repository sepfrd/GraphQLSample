using System.Net;

namespace Application.Common;

public record QueryReferenceResponse<T> where T : class
{
    public QueryReferenceResponse(T? Data = null,
        bool IsSuccessful = false,
        string Message = "",
        HttpStatusCode HttpStatusCode = HttpStatusCode.BadRequest)
    {
        this.Data = Data;
        this.IsSuccessful = IsSuccessful;
        this.Message = Message;
        this.HttpStatusCode = HttpStatusCode;
    }

    public T? Data { get; init; }

    public bool IsSuccessful { get; init; }

    public string Message { get; init; }

    public HttpStatusCode HttpStatusCode { get; init; }

    public void Deconstruct(out T? Data, out bool IsSuccessful, out string Message, out HttpStatusCode HttpStatusCode)
    {
        Data = this.Data;
        IsSuccessful = this.IsSuccessful;
        Message = this.Message;
        HttpStatusCode = this.HttpStatusCode;
    }
}