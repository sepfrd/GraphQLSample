namespace Web.GraphQL.Types.InputTypes;

public record AddProductVoteInputType
{
    public Domain.Enums.VoteType VoteType { get; set; }

    public int ProductExternalId { get; set; }
}