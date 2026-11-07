using FluentValidation;

namespace API.Application.Features.Catalog.Units.Commands.Update
{
    public class UpdateUnitCommandValidator
        : AbstractValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator()
        {
            RuleFor(x => x.Body.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Body.Symbol)
                .NotEmpty().WithMessage("Symbol is required.")
                .MaximumLength(20).WithMessage("Symbol must not exceed 20 characters.");

            RuleFor(x => x.Body.ConversionRate)
                .GreaterThan(0).WithMessage("Conversion rate must be greater than 0.");
        }
    }
}
