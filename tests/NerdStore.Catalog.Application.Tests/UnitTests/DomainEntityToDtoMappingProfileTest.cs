using AutoMapper;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Application.Models;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.Tests.UnitTests
{
    public class DomainEntityToDtoMappingProfileTest
    {
        private readonly IMapper _mapper;

        public DomainEntityToDtoMappingProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainEntityToDtoMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapProductToProductDto()
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
            Assert.Equal(product.StockQuantity.Value, productDto.StockQuantity.Value);
            Assert.Equal(product.Dimension.Height.Value, productDto.Height.Value);
            Assert.Equal(product.Dimension.Width.Value, productDto.Width.Value);
            Assert.Equal(product.Dimension.Depth.Value, productDto.Depth.Value);
        }

        [Fact]
        public void ShouldMapProductsToProductsDto()
        {
            // Arrange
            var products = new List<Product>
            {
                Product.CreateProduct("Test Product", "Test Description", true, 19.99m, 10, Category.Create("Test Category", 0), "test.jpg", 50, 10, 10),
                Product.CreateProduct("Test Product 2", "Test Description 2", false, 29.99m, 20, Category.Create("Test Category 2", 1), "test2.jpg", 60, 20, 20)
            };

            // Act
            var productsViewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products).ToList();

            // Assert
            Assert.NotNull(productsViewModel);
            Assert.Equal(products.Count, productsViewModel.Count());

            for (int index = 0; index < products.Count; index++)
            {
                Assert.Equal(products[index].Id.Value, productsViewModel[index].Id.Value);
                Assert.Equal(products[index].CategoryId.Value, productsViewModel[index].CategoryId.Value);
                Assert.Equal(products[index].Name.Value, productsViewModel[index].Name.Value);
                Assert.Equal(products[index].Description.Value, productsViewModel[index].Description.Value);
                Assert.Equal(products[index].Active, productsViewModel[index].Active);
                Assert.Equal(products[index].Price.Value, productsViewModel[index].Price.Value);
                Assert.Equal(products[index].RegisterDate.Value, productsViewModel[index].RegisterDate.Value);
                Assert.Equal(products[index].Image.Value, productsViewModel[index].Image.Value);
                Assert.Equal(products[index].StockQuantity.Value, productsViewModel[index].StockQuantity.Value);
                Assert.Equal(products[index].Dimension.Height.Value, productsViewModel[index].Height.Value);
                Assert.Equal(products[index].Dimension.Width.Value, productsViewModel[index].Width.Value);
                Assert.Equal(products[index].Dimension.Depth.Value, productsViewModel[index].Depth.Value);
            }
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
            Assert.Equal(product.StockQuantity.Value, productDto.StockQuantity.Value);
            Assert.Equal(product.Dimension.Height.Value, productDto.Height.Value);
            Assert.Equal(product.Dimension.Width.Value, productDto.Width.Value);
            Assert.Equal(product.Dimension.Depth.Value, productDto.Depth.Value);
        }

        [Fact]
        public void ShouldMapCategoryToCategoryDto()
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
