using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Questions.Handlers;

public class GetAllQuestionsQueryHandler(IRepository<Question> questionRepository)
    : BaseGetAllQueryHandler<Question>(questionRepository);