using Domain.Common;

namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public required string Username { get; set; }

    public required string Password { get; set; }
    
    public required string Email { get; set; }

    public bool IsEmailConfirmed { get; set; }
    
    public int Score { get; set; }

    public Guid PersonId { get; set; }

    public Guid CartId { get; set; }

    public ICollection<Guid>? OrderIds { get; set; }

    public ICollection<Guid>? AddressIds { get; set; }

    public ICollection<Guid>? PhoneNumberIds { get; set; }

    public ICollection<Guid>? QuestionIds { get; set; }

    public ICollection<Guid>? AnswerIds { get; set; }

    public ICollection<Guid>? VoteIds { get; set; }
}