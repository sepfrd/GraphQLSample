using Domain.Common;
using Domain.Interfaces;

namespace Domain.Entities;

public class Favorite : BaseEntity
{
    public User? User { get; set; }
    public IVotableContent? Content { get; set; }
}