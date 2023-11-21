using Application.EntityManagement.Answers.Dtos;

namespace Application.EntityManagement.Questions.Dtos;

public record QuestionDto(
    int ExternalId,
    int UserExternalId,
    string Title,
    string Description,
    IEnumerable<AnswerDto> Answers);