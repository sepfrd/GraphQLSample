using FluentValidation;

namespace Application.EntityManagement.Roles.Dtos.RoleDto;

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(40)
            .WithMessage("Title cannot exceed 40 characters.");

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");
    }
}