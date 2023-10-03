using Application.EntityManagement.Answers.Dtos;
using MediatR;

namespace Application.EntityManagement.Answers.Commands.CreateAnswer;

public abstract record CreateAnswerCommand(AnswerDto AnswerDto) : IRequest<AnswerDto?>;