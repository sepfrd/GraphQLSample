using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Score { get; set; }

    public Guid PersonId { get; set; }
    public ICollection<Guid>? AddressIds { get; set; }
    public ICollection<Guid>? PhoneNumberIds { get; set; }
    public ICollection<Guid>? QuestionIds { get; set; }
    public ICollection<Guid>? AnswerIds { get; set; }
    public ICollection<Guid>? VoteIds { get; set; }
    public ICollection<Guid>? FavoriteIds { get; set; }
}