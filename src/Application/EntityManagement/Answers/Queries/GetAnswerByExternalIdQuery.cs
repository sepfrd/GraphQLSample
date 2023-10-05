using Application.Common.Queries;
using Application.EntityManagement.Answers.Dtos;

namespace Application.EntityManagement.Answers.Queries;

public record GetAnswerByExternalIdQuery(int Id)
    : BaseGetByExternalIdQuery<AnswerDto>(Id);