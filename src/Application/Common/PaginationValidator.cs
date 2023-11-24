using Domain.Common;
using FluentValidation;

namespace Application.Common;

public class PaginationValidator : AbstractValidator<Pagination>
{
    public PaginationValidator()
    {
        RuleFor(pagination => pagination.PageNumber)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(1000);
        
        RuleFor(pagination => pagination.PageSize)
            .GreaterThanOrEqualTo(10)
            .LessThanOrEqualTo(100);
    }
}