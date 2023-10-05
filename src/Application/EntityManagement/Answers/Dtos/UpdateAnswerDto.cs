namespace Application.EntityManagement.Answers.Dtos;

public abstract record UpdateAnswerDto : AnswerDto
{
    public required int Id { get; init; }
}