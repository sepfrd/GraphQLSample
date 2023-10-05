using Application.Common.Queries;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Queries;

public record GetAnswerByInternalIdQuery(Guid Id)
    : BaseGetByInternalIdQuery<Answer>(Id);