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
                cfg.AddProfile<ViewModelToDomainEntityMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapProductViewModelToProduct()
        {
            // Arrange
            var productViewModel = new ProductViewModel
            (
                new Models.ProductId(Guid.NewGuid()),
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
                new CategoryViewModel(
                    new Models.CategoryId(Guid.NewGuid()),
                    new CategoryName("Electronics"),
                    new CategoryCode(123))
            );

            // Act
            var product = _mapper.Map<Product>(productViewModel);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(productViewModel.Id.Value, product.Id.Value);
            Assert.Equal(productViewModel.Name.Value, product.Name.Value);
            Assert.Equal(productViewModel.Description.Value, product.Description.Value);
            Assert.Equal(productViewModel.Active, product.Active);
            Assert.Equal(productViewModel.Price.Value, product.Price.Value);
            Assert.Equal(productViewModel.RegisterDate.Value, product.RegisterDate.Value);
            Assert.Equal(productViewModel.Image.Value, product.Image.Value);
            Assert.Equal(productViewModel.CategoryId.Value, product.CategoryId.Value);
            Assert.Equal(productViewModel.Height.Value, product.Dimension.Height.Value);
            Assert.Equal(productViewModel.Width.Value, product.Dimension.Width.Value);
            Assert.Equal(productViewModel.Depth.Value, product.Dimension.Depth.Value);
            Assert.Equal(productViewModel.Category.Name.Value, product.Category.Name.Value);
            Assert.Equal(productViewModel.Category.Code.Value, product.Category.Code.Value);
        }

        [Fact]
        public void ShouldMapCategoryViewModelToCategory()
        {
            // Arrange
            var categoryViewModel = new CategoryViewModel(
                new Models.CategoryId(Guid.NewGuid()),
                new CategoryName("Electronics"),
                new CategoryCode(123));

            // Act
            var category = _mapper.Map<Category>(categoryViewModel);

            // Assert
            Assert.NotNull(category);
            Assert.Equal(categoryViewModel.Id.Value, category.Id.Value);
            Assert.Equal(categoryViewModel.Name.Value, category.Name.Value);
            Assert.Equal(categoryViewModel.Code.Value, category.Code.Value);
        }
    }
}
