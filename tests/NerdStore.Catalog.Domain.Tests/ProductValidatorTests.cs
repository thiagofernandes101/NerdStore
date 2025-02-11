using FluentValidation.Results;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core;

namespace NerdStore.Catalog.Domain.Tests
{
    public class ProductValidatorTests
    {
        [Fact]
        public void ProductValidator_ShouldReturnValidForCorrectProduct()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 1, 1)
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
                ProductName.NewProductName(""),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 1, 1)
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
                ProductName.NewProductName("Valid name"),
                Description.NewDescription(""),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 1, 1)
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
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(-1),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 1, 1)
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
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash(""),
                Dimension.NewDimension(1, 1, 1)
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
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 1, 1)
            );

            // Assert
            Assert.Throws<DomainException>(() => product.DebitStock(11));
        }

        [Fact]
        public void ProductValidator_ShouldReturnErrorForEmptyCategoryId()
        {
            // Arrange
            var product = Product.NewProduct(
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.Empty,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 1, 1)
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
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(0, 1, 1)
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
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 0, 1)
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
                ProductName.NewProductName("Valid name"),
                Description.NewDescription("Valid description"),
                true,
                Price.NewPrice(10),
                Stock.NewStock(10),
                CategoryId.NewId,
                RegisterDate.Now,
                ImageHash.NewImageHash("validhash"),
                Dimension.NewDimension(1, 1, 0)
            );
            // Act
            ValidationResult result = product.IsValid();
            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Depth must be greater than zero.");
        }
    }
}
