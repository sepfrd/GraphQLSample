namespace Application.EntityManagement.Answers.Dtos.AnswerDto;

public record AnswerDto(
    int QuestionExternalId,
    string Title,
    string Description);