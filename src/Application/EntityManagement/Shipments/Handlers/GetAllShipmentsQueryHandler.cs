using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Shipments.Handlers;

public class GetAllShipmentsQueryHandler(IRepository<Shipment> shipmentRepository)
    : BaseGetAllQueryHandler<Shipment>(shipmentRepository);