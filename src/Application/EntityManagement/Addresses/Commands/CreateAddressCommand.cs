using Application.Common;
using Application.EntityManagement.Addresses.Dtos.AddressDto;
using MediatR;

namespace Application.EntityManagement.Addresses.Commands;

public record CreateAddressCommand(AddressDto AddressDto) : IRequest<CommandResult>;