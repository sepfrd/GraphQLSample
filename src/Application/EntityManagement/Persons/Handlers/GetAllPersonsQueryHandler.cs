using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Persons.Handlers;

public class GetAllPersonsQueryHandler(IRepository<Person> personRepository)
    : BaseGetAllQueryHandler<Person>(personRepository);