using Application.Common.Commands;
using Application.EntityManagement.Persons.Dtos;

namespace Application.EntityManagement.Persons.Commands;

public record UpdatePersonCommand(int ExternalId, PersonDto Dto) : BaseUpdateCommand<PersonDto>(ExternalId, Dto);