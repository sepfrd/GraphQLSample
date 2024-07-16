using FluentValidation;

namespace Application.EntityManagement.UserRoles.Dtos.UserRoleDto;

public class UserRoleDtoValidator : AbstractValidator<UserRoleDto>
{
    public UserRoleDtoValidator()
    {
        RuleFor(dto => dto.UserExternalId)
            .GreaterThan(0)
            .WithMessage("UserExternalId should be greater than 0.");

        RuleFor(dto => dto.RoleExternalId)
            .GreaterThan(0)
            .WithMessage("RoleExternalId should be greater than 0.");
    }
}