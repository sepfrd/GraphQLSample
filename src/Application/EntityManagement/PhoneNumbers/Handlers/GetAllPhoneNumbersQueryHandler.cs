using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class GetAllPhoneNumbersQueryHandler(IRepository<PhoneNumber> phoneNumberRepository)
    : BaseGetAllQueryHandler<PhoneNumber>(phoneNumberRepository);