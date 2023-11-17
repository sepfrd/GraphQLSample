namespace Web.GraphQL.Types.InputTypes;

public record AddAnswerVoteInputType
{
    public Domain.Enums.VoteType VoteType { get; set; }
    public int AnswerExternalId { get; set; }
}