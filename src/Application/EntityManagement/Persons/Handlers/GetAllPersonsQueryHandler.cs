using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Persons.Handlers;

public class GetAllPersonsQueryHandler : BaseGetAllQueryHandler<Person>
{
    public GetAllPersonsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}