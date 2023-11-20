using Domain.Common;

namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string Email { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public int Score { get; set; }

    public Person? Person { get; set; }

    public Guid PersonId { get; set; }

    public Cart? Cart { get; set; }

    public Guid CartId { get; set; }

    public ICollection<Order>? Orders { get; set; }

    public ICollection<Address>? Addresses { get; set; }

    public ICollection<PhoneNumber>? PhoneNumbers { get; set; }

    public ICollection<Question>? Questions { get; set; }

    public ICollection<Answer>? Answers { get; set; }

    public ICollection<Vote>? Votes { get; set; }

    public ICollection<Payment>? Payments { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}