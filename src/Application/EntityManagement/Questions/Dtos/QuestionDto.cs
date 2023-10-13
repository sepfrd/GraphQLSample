using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.Votes.Dtos;

namespace Application.EntityManagement.Questions.Dtos;

public record QuestionDto
(
    int ExternalId,
    int UserExternalId,
    string Title,
    string Description,
    ICollection<AnswerDto> AnswerDtos,
    ICollection<VoteDto> VoteDtos
);