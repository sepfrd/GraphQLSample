using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Payments.Handlers;

public class GetAllPaymentsQueryHandler : BaseGetAllQueryHandler<Payment>
{
    public GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}