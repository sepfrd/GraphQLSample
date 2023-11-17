using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Handlers;

public class GetAllAnswersQueryHandler(IRepository<Answer> answerRepository)
    : BaseGetAllQueryHandler<Answer>(answerRepository);