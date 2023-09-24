using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Score { get; set; }

    public Person? Person { get; set; }
    public Cart? Cart { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Address>? Addresses { get; set; }
    public ICollection<PhoneNumber>? PhoneNumbers { get; set; }
    public ICollection<Question>? Questions { get; set; }
    public ICollection<Answer>? Answers { get; set; }
    public ICollection<Vote>? Votes { get; set; }
    public ICollection<Favorite>? Favorites { get; set; }
}