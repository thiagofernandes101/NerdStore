using FluentValidation;
using NerdStore.Catalog.Domain.ValueObjects;

namespace NerdStore.Catalog.Domain.Validations
{
    public class DimensionValidator : AbstractValidator<ProductDimension>
    {
        public DimensionValidator()
        {
            RuleFor(property => property.Height.Value)
                .GreaterThan(0)
                .WithMessage("Height must be greater than zero.");

            RuleFor(property => property.Width.Value)
                .GreaterThan(0)
                .WithMessage("Width must be greater than zero.");

            RuleFor(property => property.Depth.Value)
                .GreaterThan(0)
                .WithMessage("Depth must be greater than zero.");
        }
    }
}
