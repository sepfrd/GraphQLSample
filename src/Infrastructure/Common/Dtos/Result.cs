namespace Infrastructure.Common.Dtos;

public record Result<T>(T? Data, string? Message, int StatusCode);