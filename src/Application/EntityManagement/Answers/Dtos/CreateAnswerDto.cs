namespace Application.EntityManagement.Answers.Dtos;

public record CreateAnswerDto(
    int QuestionExternalId,
    int UserExternalId,
    string Title,
    string Description
);