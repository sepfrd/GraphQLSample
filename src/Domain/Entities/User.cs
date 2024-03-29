using Domain.Common;

namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public required string Username { get; init; }

    public required string Password { get; init; }

    public required string Email { get; init; }

    public bool IsEmailConfirmed { get; init; }

    public int Score { get; init; }

    public Person? Person { get; set; }

    public Guid PersonId { get; set; }

    public Cart? Cart { get; set; }

    public Guid CartId { get; set; }

    public ICollection<Order>? Orders { get; set; }

    public ICollection<Address>? Addresses { get; init; }

    public ICollection<PhoneNumber>? PhoneNumbers { get; init; }

    public ICollection<Question>? Questions { get; set; }

    public ICollection<Answer>? Answers { get; set; }

    public ICollection<Vote>? Votes { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}