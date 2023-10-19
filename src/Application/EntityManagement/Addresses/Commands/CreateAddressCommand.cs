using Application.Common.Commands;
using Application.EntityManagement.Addresses.Dtos;

namespace Application.EntityManagement.Addresses.Commands;

public record CreateAddressCommand(AddressDto AddressDto) : BaseCreateCommand<AddressDto>(AddressDto);