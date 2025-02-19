using AutoMapper;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Application.Models;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.Tests.UnitTests
{
    public class ViewModelToDomainEntityMappingProfileTests
    {
        private readonly IMapper _mapper;

        public ViewModelToDomainEntityMappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToDomainEntityMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapProductViewModelToProduct()
        {
            // Arrange
            var productDto = new ProductViewModel
            (
                new Models.ProductId(Guid.NewGuid()),
                new ProductCategoryId(Guid.NewGuid()),
                new ProductName("Test Product"),
                new ProductDescription("Test Description"),
                true,
                new ProductPrice(19.99m),
                new ProductRegisterDate(DateTime.Now),
                new ProductImage("test.jpg"),
                new ProductStockQuantity(50),
                new ProductHeight(10),
                new ProductWidth(10),
                new ProductDepth(10),
                new CategoryModel(
                    new Models.CategoryId(Guid.NewGuid()),
                    new CategoryName("Electronics"),
                    new CategoryCode(123))
            );

            // Act
            var product = _mapper.Map<Product>(productDto);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(productDto.Id.Value, product.Id.Value);
            Assert.Equal(productDto.Name.Value, product.Name.Value);
            Assert.Equal(productDto.Description.Value, product.Description.Value);
            Assert.Equal(productDto.Active, product.Active);
            Assert.Equal(productDto.Price.Value, product.Price.Value);
            Assert.Equal(productDto.RegisterDate.Value, product.RegisterDate.Value);
            Assert.Equal(productDto.Image.Value, product.Image.Value);
            Assert.Equal(productDto.CategoryId.Value, product.CategoryId.Value);
            Assert.Equal(productDto.Height.Value, product.Dimension.Height.Value);
            Assert.Equal(productDto.Width.Value, product.Dimension.Width.Value);
            Assert.Equal(productDto.Depth.Value, product.Dimension.Depth.Value);
            Assert.Equal(productDto.Category.Name.Value, product.Category.Name.Value);
            Assert.Equal(productDto.Category.Code.Value, product.Category.Code.Value);
        }

        [Fact]
        public void ShouldMapCategoryViewModelToCategory()
        {
            // Arrange
            var categoryDto = new CategoryModel(
                new Models.CategoryId(Guid.NewGuid()),
                new CategoryName("Electronics"),
                new CategoryCode(123));

            // Act
            var category = _mapper.Map<Category>(categoryDto);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(categoryDto.Id.Value, category.Id.Value);
            Assert.Equal(categoryDto.Name.Value, category.Name.Value);
            Assert.Equal(categoryDto.Code.Value, category.Code.Value);
        }
    }
}
