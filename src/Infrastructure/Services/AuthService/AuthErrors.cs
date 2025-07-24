using Domain.Abstractions;

namespace Infrastructure.Services.AuthService;

public static class AuthErrors
{
    public static Error PropertyNotUnique(string propertyName, string propertyValue) => new($"{propertyValue} is already taken. Choose another {propertyName}.");
}