using AutoMapper;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.Tests.AutoMapper
{
    public class DtoToDomainEntityMappingProfileTests
    {
        private readonly IMapper _mapper;

        public DtoToDomainEntityMappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToDomainEntityMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapProductDtoToProduct()
        {
            // Arrange
            var productDto = new ProductDto
            (
                new ProductDtoId(Guid.NewGuid()),
                new ProductDtoCategoryId(Guid.NewGuid()),
                new ProductDtoName("Test Product"),
                new ProductDtoDescription("Test Description"),
                true,
                new ProductDtoPrice(19.99m),
                new ProductDtoRegisterDate(DateTime.Now),
                new ProductDtoImage("test.jpg"),
                new ProductDtoStockQuantity(50),
                new ProductDtoHeight(10),
                new ProductDtoWidth(10),
                new ProductDtoDepth(10)
            );

            // Act
            var product = _mapper.Map<Product>(productDto);

            // Assert
            Assert.NotNull(product);
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
        }

        [Fact]
        public void ShouldMapCategoryDtoToCategory()
        {
            // Arrange
            var categoryDto = new CategoryDto(
                new CategoryDtoId(Guid.NewGuid()),
                new CategoryDtoName("Electronics"),
                new CategoryDtoCode(123));

            // Act
            var category = _mapper.Map<Category>(categoryDto);

            // Assert
            Assert.Equal(categoryDto.Name.Value, category.Name.Value);
            //Assert.Equal(categoryDto.Code.Value, category.Code.Value);
        }
    }
}
