using Domain.Common;
using Domain.Entities;

namespace Domain;

public class Question : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }
    public List<Answer> Answers { get; set; } = new();
    public List<Vote> Votes { get; set; } = new();
}
