using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.CartItems.Handlers;

public class GetAllCartItemsQueryHandler : BaseGetAllQueryHandler<CartItem>
{
    public GetAllCartItemsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}