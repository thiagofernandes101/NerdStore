using FluentValidation;

namespace NerdStore.Catalog.Domain.Validations
{
    public class DimensionValidator : AbstractValidator<Dimension>
    {
        public DimensionValidator()
        {
            RuleFor(property => property.Height.Value)
                .GreaterThan(0)
                .WithMessage("The height must be greater than zero.");

            RuleFor(property => property.Width.Value)
                .GreaterThan(0)
                .WithMessage("The width must be greater than zero.");

            RuleFor(property => property.Depth.Value)
                .GreaterThan(0)
                .WithMessage("The depth must be greater than zero.");
        }
    }
}
