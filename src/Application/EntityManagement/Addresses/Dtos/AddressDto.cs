namespace Application.EntityManagement.Addresses.Dtos;

public sealed record AddressDto
(
    int ExternalId,
    string? Street,
    string? City,
    string? State,
    string? PostalCode,
    string? Country,
    string? UnitNumber,
    string? BuildingNumber
);