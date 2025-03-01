using AutoMapper;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Application.Models;
using NerdStore.Catalog.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NerdStore.Catalog.Application.Tests.UnitTests
{
    public class DomainEntityToViewModelMappingProfileTest
    {
        private readonly IMapper _mapper;

        public DomainEntityToViewModelMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainEntityToDtoMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapProductToProductViewModel()
        {
            // Arrange
            var product = Product.CreateProduct
            (
                "Test Product",
                "Test Description",
                true,
                19.99m,
                10,
                Category.Create("Test Category", 0),
                "test.jpg",
                50,
                10,
                10
            );

            // Act
            var productDto = _mapper.Map<ProductViewModel>(product);

            // Assert
            Assert.NotNull(productDto);
            Assert.Equal(product.Id.Value, productDto.Id.Value);
            Assert.Equal(product.CategoryId.Value, productDto.CategoryId.Value);
            Assert.Equal(product.Name.Value, productDto.Name.Value);
            Assert.Equal(product.Description.Value, productDto.Description.Value);
            Assert.Equal(product.Active, productDto.Active);
            Assert.Equal(product.Price.Value, productDto.Price.Value);
            Assert.Equal(product.RegisterDate.Value, productDto.RegisterDate.Value);
            Assert.Equal(product.Image.Value, productDto.Image.Value);
            Assert.Equal(product.Stock.Amount, productDto.StockQuantity.Value);
            Assert.Equal(product.Dimension.Height.Value, productDto.Height.Value);
            Assert.Equal(product.Dimension.Width.Value, productDto.Width.Value);
            Assert.Equal(product.Dimension.Depth.Value, productDto.Depth.Value);
        }

        [Fact]
        public void ShouldMapProductWithNullCategoryToProductViewModel()
        {
            // Arrange
            var product = Product.CreateProduct
            (
                "Test Product",
                "Test Description",
                true,
                19.99m,
                10,
                null,
                "test.jpg",
                50,
                10,
                10
            );

            // Act
            var productDto = _mapper.Map<ProductViewModel>(product);

            // Assert
            Assert.NotNull(productDto);
            Assert.Equal(product.Id.Value, productDto.Id.Value);
            Assert.Equal(product.CategoryId.Value, Guid.Empty);
            Assert.Equal(product.Name.Value, productDto.Name.Value);
            Assert.Equal(product.Description.Value, productDto.Description.Value);
            Assert.Equal(product.Active, productDto.Active);
            Assert.Equal(product.Price.Value, productDto.Price.Value);
            Assert.Equal(product.RegisterDate.Value, productDto.RegisterDate.Value);
            Assert.Equal(product.Image.Value, productDto.Image.Value);
            Assert.Equal(product.Stock.Amount, productDto.StockQuantity.Value);
            Assert.Equal(product.Dimension.Height.Value, productDto.Height.Value);
            Assert.Equal(product.Dimension.Width.Value, productDto.Width.Value);
            Assert.Equal(product.Dimension.Depth.Value, productDto.Depth.Value);
        }

        [Fact]
        public void ShouldMapCategoryToCategoryViewModel()
        {
            // Arrange
            var category = Category.Create("Test Category", 0);
            // Act
            var categoryDto = _mapper.Map<CategoryModel>(category);
            // Assert
            Assert.NotNull(categoryDto);
            Assert.Equal(category.Id.Value, categoryDto.Id.Value);
            Assert.Equal(category.Name.Value, categoryDto.Name.Value);
            Assert.Equal(category.Code.Value, categoryDto.Code.Value);
        }
    }
}
