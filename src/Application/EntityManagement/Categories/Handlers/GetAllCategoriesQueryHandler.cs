using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Categories.Handlers;

public class GetAllCategoriesQueryHandler : BaseGetAllQueryHandler<Category>
{
    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}