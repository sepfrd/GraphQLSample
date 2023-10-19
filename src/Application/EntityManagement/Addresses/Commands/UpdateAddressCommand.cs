using Application.Common.Commands;
using Application.EntityManagement.Addresses.Dtos;

namespace Application.EntityManagement.Addresses.Commands;

public record UpdateAddressCommand(int ExternalId, AddressDto AddressDto) : BaseUpdateCommand<AddressDto>(ExternalId, AddressDto);