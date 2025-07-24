using Domain.Abstractions;

namespace Application.Common;

public static class Errors
{
    public static readonly Error InternalServerError = new("InternalServerError");
    public static readonly Error NotFound = new("NotFound");
    public static readonly Error IdenticalValues = new("Identical Values");

    public static Error NotFoundById(string entityName, Guid id) => new($"{entityName} with ID '{id}' not found.");
}