namespace Application.EntityManagement.Addresses.Dtos;

public sealed record CreateAddressDto(
    int UserExternalId,
    string? Street,
    string? City,
    string? State,
    string? PostalCode,
    string? Country,
    string? UnitNumber,
    string? BuildingNumber
);