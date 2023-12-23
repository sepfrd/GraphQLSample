using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Comments.Dtos.CommentDto;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using Application.EntityManagement.Persons.Dtos;
using Application.EntityManagement.Persons.Dtos.PersonDto;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;

namespace Application.EntityManagement.Users.Dtos.UserDto;

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
    IEnumerable<PaymentDto>? Payments,
    IEnumerable<CommentDto>? Comments);