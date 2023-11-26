#region

using Application.EntityManagement.Answers.Dtos;

#endregion

namespace Application.EntityManagement.Questions.Dtos;

public record QuestionDto(
    int UserExternalId,
    string Title,
    string Description,
    IEnumerable<AnswerDto> Answers);