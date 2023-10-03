using Application.Abstractions;
using Application.EntityManagement.Answers.Commands.CreateAnswer;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Answers.Handlers;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, AnswerDto?>
{
    private readonly IDbContext<Answer> _dbContext;
    private readonly IMappingService _mappingService;

    public CreateAnswerCommandHandler(IDbContext<Answer> dbContext, IMappingService mappingService)
    {
        _dbContext = dbContext;
        _mappingService = mappingService;
    }

    public async Task<AnswerDto?> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        var answer = _mappingService.Map<AnswerDto, Answer>(request.AnswerDto);

        if (answer is null)
        {
            return null;
        }

        var createdAnswer = await _dbContext.CreateAsync(answer, cancellationToken);

        if (createdAnswer is null)
        {
            return null;
        }

        var answerDto = _mappingService.Map<Answer, AnswerDto>(createdAnswer);

        return answerDto;
    }
}