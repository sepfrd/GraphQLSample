#region

using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Addresses.Commands;

public record CreateAddressCommand(CreateAddressDto CreateAddressDto) : IRequest<CommandResult>;