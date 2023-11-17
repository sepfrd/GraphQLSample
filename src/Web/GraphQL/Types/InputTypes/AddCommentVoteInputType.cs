namespace Web.GraphQL.Types.InputTypes;

public record AddCommentVoteInputType
{
    public Domain.Enums.VoteType VoteType { get; set; }
    public int CommentExternalId { get; set; }
}