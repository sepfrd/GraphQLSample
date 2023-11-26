#region

using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Persons.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;

#endregion

namespace Application.EntityManagement.Users.Dtos;

public sealed record UserDto(
    int ExternalId,
    string Username,
    string Email,
    int Score,
    int OrdersCount,
    int QuestionsCount,
    int AnswersCount,
    int VotesCount,
    PersonDto? Person,
    IEnumerable<AddressDto>? Addresses,
    IEnumerable<PhoneNumberDto>? PhoneNumbers,
    IEnumerable<CreatePaymentDto>? Payments,
    IEnumerable<CommentDto>? Comments);