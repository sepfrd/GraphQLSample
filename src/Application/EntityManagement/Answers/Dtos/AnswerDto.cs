namespace Application.EntityManagement.Answers.Dtos;

public record AnswerDto
{
    public required string Title { get; init; }

    public required string Description { get; init; }

    public required int QuestionId { get; init; }

    public required int UserId { get; init; }
}