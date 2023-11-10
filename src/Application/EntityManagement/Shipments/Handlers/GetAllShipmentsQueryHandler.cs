using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Shipments.Handlers;

public class GetAllShipmentsQueryHandler : BaseGetAllQueryHandler<Shipment>
{
    public GetAllShipmentsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}