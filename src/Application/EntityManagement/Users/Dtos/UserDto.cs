using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;

namespace Application.EntityManagement.Users.Dtos;

public record UserDto
(
    int ExternalId,
    string FirstName,
    string LastName,
    string Username,
    string Email,
    int Score,
    int OrdersCount,
    int QuestionsCount,
    int AnswersCount,
    int VotesCount,
    IEnumerable<AddressDto>? AddressDtos,
    IEnumerable<PhoneNumberDto>? PhoneNumberDtos
);