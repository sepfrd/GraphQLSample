namespace Web.GraphQL.Types.InputTypes;

public record AddQuestionVoteInputType
{
    public Domain.Enums.VoteType VoteType { get; set; }
    public int QuestionExternalId { get; set; }
}