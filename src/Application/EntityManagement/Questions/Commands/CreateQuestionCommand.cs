using Application.Common;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Questions.Dtos.QuestionDto;
using MediatR;

namespace Application.EntityManagement.Questions.Commands;

public record CreateQuestionCommand(QuestionDto QuestionDto) : IRequest<CommandResult>;