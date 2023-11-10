using Application.Common.Commands;
using Application.EntityManagement.Persons.Dtos;
using Domain.Entities;

namespace Application.EntityManagement.Persons.Commands;

public record CreatePersonCommand(PersonDto Dto) : BaseCreateCommand<PersonDto>(Dto);