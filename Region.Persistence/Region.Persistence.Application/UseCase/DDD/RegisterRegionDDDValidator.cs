using FluentValidation;
using Region.Persistence.Communication.Request;
using Region.Persistence.Exceptions;

namespace Region.Persistence.Application.UseCase.DDD;
public class RegisterRegionDDDValidator : AbstractValidator<RequestRegionDDDJson>
{
    public RegisterRegionDDDValidator()
    {
        RuleFor(p => p.Region)
            .NotNull()
            .WithMessage(ErrorsMessages.RegionNotEmpty);

        RuleFor(p => p.Region)
            .IsInEnum()
            .WithMessage(ErrorsMessages.InvalidRegion);
        RuleFor(p => p.DDD)
            .NotEmpty()
            .WithMessage(ErrorsMessages.DDDNotFound);

        RuleFor(p => p.DDD)
            .InclusiveBetween(10, 99)
            .WithMessage(ErrorsMessages.DDDBetweenTenNinetyNine);
    }
}
