using Application.EntityManagement.Answers.Dtos;

namespace Application.EntityManagement.Questions.Dtos;

public record QuestionDto(string Title, string Description, IEnumerable<AnswerDto> Answers);