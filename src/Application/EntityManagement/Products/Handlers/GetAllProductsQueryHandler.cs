using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Products.Handlers;

public class GetAllProductsQueryHandler : BaseGetAllQueryHandler<Product>
{
    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}