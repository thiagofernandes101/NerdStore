using AutoMapper;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Bus;
using NSubstitute;

namespace NerdStore.Catalog.Application.Tests.IntegrationTests
{
    public class ProductApplicationServiceTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _testDatabaseFixture;

        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        private readonly IMediatRHandler _mockMediaRHandler;
        private readonly ProductApplicationService _productApplicationService;

        public ProductApplicationServiceTests(TestDatabaseFixture testDatabaseFixture)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainEntityToDtoMappingProfile>();
                cfg.AddProfile<DtoToDomainEntityMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _mockMediaRHandler = Substitute.For<IMediatRHandler>();

            _testDatabaseFixture = testDatabaseFixture;
            _productRepository = new ProductRepository(_testDatabaseFixture.CreateContext());
            _stockService = new StockService(_productRepository, _mockMediaRHandler);
            _productApplicationService = new ProductApplicationService(_productRepository, _stockService, _mapper);
        }

        [Fact]
        public async Task ShouldReturnAllProducts()
        {
            // Act
            var result = await _productApplicationService.GetAll();

            // Assert
            Assert.True(result.Any());
        }
    }
}
