using Application.Common.Commands;
using Application.EntityManagement.Answers.Dtos;

namespace Application.EntityManagement.Answers.Commands.DeleteAnswer;

public abstract record DeleteAnswerByExternalIdCommand(int Id) : BaseDeleteByExternalIdCommand<AnswerDto>(Id);