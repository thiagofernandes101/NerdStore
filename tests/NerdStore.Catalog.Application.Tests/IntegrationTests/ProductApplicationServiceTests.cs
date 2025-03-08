using AutoMapper;
using NSubstitute;
using Application = NerdStore.Catalog.Application;
using Domain = NerdStore.Catalog.Domain;
using Data = NerdStore.Catalog.Data;
using CoreDomain = NerdStore.Core;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Tests.IntegrationTests
{
    public class ProductApplicationServiceTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _testDatabaseFixture;
        private readonly IMapper _mapper;
        private readonly CoreDomain.Bus.IMediatRHandler _mockMediaRHandler;
        private Domain.Repositories.IProductRepository _productRepository;
        private Domain.Services.IStockService _stockService;
        private Application.Services.ProductApplicationService _productApplicationService;

        public ProductApplicationServiceTests(TestDatabaseFixture testDatabaseFixture)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Application.AutoMapper.DomainEntityToDtoMappingProfile>();
                cfg.AddProfile<Application.AutoMapper.ViewModelToDomainEntityMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _mockMediaRHandler = Substitute.For<CoreDomain.Bus.IMediatRHandler>();

            _testDatabaseFixture = testDatabaseFixture;
            _productRepository = new Data.Repositories.ProductRepository(_testDatabaseFixture.CreateContext());
            _stockService = new Domain.Services.StockService(_productRepository, _mockMediaRHandler);
            _productApplicationService = new Application.Services.ProductApplicationService(_productRepository, _stockService, _mapper);
        }

        [Fact]
        public async Task ShouldAddProduct()
        {
            // Arrange
            var context = _testDatabaseFixture.CreateContext();
            var product = new Application.Models.ProductViewModel
            (
                new Application.Models.ProductId(Guid.NewGuid()),
                new Application.Models.ProductName("Product 1"),
                new Application.Models.ProductDescription("Description"),
                true,
                new Application.Models.ProductPrice(100),
                new Application.Models.ProductRegisterDate(DateTime.Now),
                new Application.Models.ProductImage("image.png"),
                new Application.Models.ProductStockQuantity(10),
                new Application.Models.ProductHeight(10),
                new Application.Models.ProductWidth(10),
                new Application.Models.ProductDepth(10),
                new Application.Models.CategoryViewModel
                (
                    new Application.Models.CategoryId(Guid.NewGuid()),
                    new Application.Models.CategoryName("Category 1"),
                    new Application.Models.CategoryCode(3)
                )
            );

            context.Database.BeginTransaction();
            
            _productRepository = new Data.Repositories.ProductRepository(context);
            _stockService = new Domain.Services.StockService(_productRepository, _mockMediaRHandler);
            _productApplicationService = new Application.Services.ProductApplicationService(_productRepository, _stockService, _mapper);

            // Act
            await _productApplicationService.AddProduct(product);
            context.ChangeTracker.Clear();
            
            // Assert
            var productAdded = await _productApplicationService.GetById(product.Id);
            Assert.NotNull(productAdded);
            Assert.Equal(product.Id.Value, productAdded.Id.Value);
            Assert.Equal(product.Name.Value, productAdded.Name.Value);
            Assert.Equal(product.Description.Value, productAdded.Description.Value);
            Assert.Equal(product.Active, productAdded.Active);
            Assert.Equal(product.Price.Value, productAdded.Price.Value);
            Assert.Equal(product.RegisterDate.Value.ToString("f"), productAdded.RegisterDate.Value.ToString("f"));
            Assert.Equal(product.Image.Value, productAdded.Image.Value);
            Assert.Equal(product.StockQuantity.Value, productAdded.StockQuantity.Value);
            Assert.Equal(product.Height.Value, productAdded.Height.Value);
            Assert.Equal(product.Width.Value, productAdded.Width.Value);
            Assert.Equal(product.Depth.Value, productAdded.Depth.Value);
        }

        [Fact]
        public async Task ShouldReturnAllProducts()
        {
            // Act
            var result = await _productApplicationService.GetAll();

            // Assert
            Assert.True(result.Any());
        }

        [Fact]
        public async Task ShouldReturnAllCathegoriesWithSpecifiedByCode()
        {
            // Act
            var result = await _productApplicationService.GetByCategory(new Application.Models.CategoryCode(2));

            // Assert
            Assert.True(result.Any());
            Assert.True(result.All(x => x.Category.Code.Value == 2));
        }
    }
}
