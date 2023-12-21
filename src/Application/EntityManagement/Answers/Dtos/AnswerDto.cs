namespace Application.EntityManagement.Answers.Dtos;

public record AnswerDto(
    int QuestionExternalId,
    string Title,
    string Description);