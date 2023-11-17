using Application.Common.Commands;
using Application.EntityManagement.Persons.Dtos;

namespace Application.EntityManagement.Persons.Commands;

public record CreatePersonCommand(PersonDto Dto) : BaseCreateCommand<PersonDto>(Dto);