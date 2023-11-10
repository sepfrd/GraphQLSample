using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class GetAllPhoneNumbersQueryHandler : BaseGetAllQueryHandler<PhoneNumber>
{
    public GetAllPhoneNumbersQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}