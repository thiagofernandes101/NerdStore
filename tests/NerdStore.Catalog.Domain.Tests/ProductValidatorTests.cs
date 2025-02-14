using FluentValidation.Results;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.Exceptions;

namespace NerdStore.Catalog.Domain.Tests
{
    public class ProductValidatorTests
    {
        [Fact]
        public void ProductValidator_ShouldReturnValidForCorrectProduct()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 1, 1)
            );
            // Act
            ValidationResult result = product.IsValid();
            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForEmptyProductName()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create(""),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 1, 1)
            );

            // Act
            ValidationResult result = product.IsValid();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Product name cannot be empty.");
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForEmptyDescription()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create(""),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 1, 1)
            );

            // Act
            ValidationResult result = product.IsValid();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Description cannot be empty");
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForNegativePrice()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(-1),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 1, 1)
            );

            // Act
            ValidationResult result = product.IsValid();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Price must be greater then zero.");
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForEmptyImageHash()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create(""),
                Dimension.Create(1, 1, 1)
            );

            // Act
            ValidationResult result = product.IsValid();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Product image cannot be empry.");
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForNegativeStock()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 1, 1)
            );

            // Assert
            Assert.Throws<DomainException>(() => product.DebitStock(11));
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForEmptyCategoryId()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.Empty,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 1, 1)
            );

            // Act
            ValidationResult result = product.IsValid();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Category is required.");
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForInvalidHeight()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(0, 1, 1)
            );
            // Act
            ValidationResult result = product.IsValid();
            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Height must be greater than zero.");
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForInvalidWidth()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 0, 1)
            );
            // Act
            ValidationResult result = product.IsValid();
            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Width must be greater than zero.");
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForInvalidDepth()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.Create("Valid name"),
                Description.Create("Valid description"),
                true,
                Price.Create(10),
                StockQuantity.Create(10),
                CategoryId.NewId,
                ImageHash.Create("validhash"),
                Dimension.Create(1, 1, 0)
            );
            // Act
            ValidationResult result = product.IsValid();
            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Depth must be greater than zero.");
        }
    }
}
