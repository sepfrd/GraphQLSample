using FluentValidation;

namespace Application.EntityManagement.Votes.Dtos.VoteDto;

public class VoteDtoValidator : AbstractValidator<VoteDto>
{
    public VoteDtoValidator()
    {
        RuleFor(voteDto => voteDto.Type)
            .IsInEnum()
            .WithMessage("Invalid VoteType.");

        RuleFor(voteDto => voteDto.ContentType)
            .IsInEnum()
            .WithMessage("Invalid VotableContentType.");

        RuleFor(voteDto => voteDto.ContentExternalId)
            .GreaterThan(0)
            .WithMessage("ContentExternalId should be greater than 0.");
    }
}