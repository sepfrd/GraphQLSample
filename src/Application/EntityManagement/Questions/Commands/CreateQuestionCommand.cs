#region

using Application.Common;
using Application.EntityManagement.Questions.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Questions.Commands;

public record CreateQuestionCommand(QuestionDto QuestionDto) : IRequest<CommandResult>;