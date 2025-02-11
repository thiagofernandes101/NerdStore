using FluentValidation;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Domain.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .Must(name => !string.IsNullOrWhiteSpace(name.Value))
                .WithMessage("Product name cannot be empty.");

            RuleFor(p => p.Description)
                .NotNull()
                .Must(x => !string.IsNullOrWhiteSpace(x.Value))
                .WithMessage("Description cannot be empty");

            RuleFor(p => p.Price)
                .NotNull()
                .Must(price => price.Value >= 0)
                .WithMessage("Price must be greater then zero.");

            RuleFor(p => p.Image)
                .NotNull()
                .Must(hash => !string.IsNullOrWhiteSpace(hash.Value))
                .WithMessage("Product image cannot be empry.");

            RuleFor(p => p.StockQuantity)
                .NotNull().WithMessage("Stock quantity is required.")
                .Must(stock => stock.Value >= 0)
                .WithMessage("Stock quantity cannot be negative.");

            RuleFor(p => p.CategoryId)
                .Must(x => x.Value != Guid.Empty)
                .WithMessage("Category is required.");

            RuleFor(p => p.Dimension)
                .SetValidator(new DimensionValidator());
        }
    }
}