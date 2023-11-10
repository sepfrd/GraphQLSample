using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Carts.Handlers;

public class GetAllCartsQueryHandler : BaseGetAllQueryHandler<Cart>
{
    public GetAllCartsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}