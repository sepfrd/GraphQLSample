using Application.Common;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Events;

public record AnswerDeletedEvent(Answer Entity) : EntityDeletedEvent<Answer>(Entity);