using Domain.Abstractions;

namespace Application.Common;

public static class Errors
{
    public static readonly Error InternalServerError = new("500", "InternalServerError");
    public static readonly Error NotFound = new("404", "NotFound");
    public static readonly Error IdenticalValues = new("400", "Identical Values");

    public static Error NotFoundById(string entityName, Guid id) => new("404", $"{entityName} with ID '{id}' not found.");
}