using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;

namespace Application.EntityManagement.Users.Dtos;

public sealed record UserDto
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
    IEnumerable<PhoneNumberDto>? PhoneNumberDtos,
    IEnumerable<PaymentDto>? PaymentDtos,
    IEnumerable<CommentDto>? CommentDtos
);