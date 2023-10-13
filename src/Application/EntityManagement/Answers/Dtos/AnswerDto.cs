namespace Application.EntityManagement.Answers.Dtos;

public record AnswerDto
{
    public int ExternalId { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required int QuestionExternalId { get; init; }

    public required int UserExternalId { get; init; }
}