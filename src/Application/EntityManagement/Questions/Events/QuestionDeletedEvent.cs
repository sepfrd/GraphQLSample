using Application.Common;
using Domain.Entities;

namespace Application.EntityManagement.Questions.Events;

public record QuestionDeletedEvent(Question Entity) : EntityDeletedEvent<Question>(Entity);