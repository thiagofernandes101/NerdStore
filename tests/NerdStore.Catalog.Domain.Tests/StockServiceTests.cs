using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Bus;
using NerdStore.Core.DomainObjects;
using NSubstitute;

namespace NerdStore.Catalog.Domain.Tests
{
    public class StockServiceTests
    {
        private readonly IProductRepository _productRepositoryMock;
        private readonly IMediatRHandler _mediatRHandlerMock;
        private readonly StockService _stockService;

        public StockServiceTests()
        {
            _productRepositoryMock = Substitute.For<IProductRepository>();
            _mediatRHandlerMock = Substitute.For<IMediatRHandler>();
            _stockService = new StockService(_productRepositoryMock, _mediatRHandlerMock);
        }

        [Fact]
        public async Task DebitStock_ShouldReturnFalse_WhenProductNotFound()
        {
            // Arrange
            var productId = ProductId.NewId;
            _productRepositoryMock.GetById(productId).Returns(Result<Product>.Failure("Product not found."));

            // Act
            var result = await _stockService.DebitStock(productId, 1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Product not found.", result.Error);
        }

        [Fact]
        public async Task DebitStock_ShouldReturnFalse_WhenProductHasInsufficientStock()
        {
            // Arrange
            var product = Product.CreateProductWithoutCategory(
                "Product name",
                "Product description",
                true,
                19.99m,
                0,
                "image-hash",
                10,
                10,
                10);
            _productRepositoryMock.GetById(Arg.Any<ProductId>()).Returns(Result<Product>.Success(product));

            // Act
            var result = await _stockService.DebitStock(product.Id, 1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Insufficient stock.", result.Error);
        }

        [Fact]
        public async Task DebitStock_ShouldReturnTrue_WhenProductHasSufficientStock()
        {
            // Arrange
            var product = Product.CreateProductWithoutCategory(
                "Product name",
                "Product description",
                true,
                19.99m,
                20,
                "image-hash",
                10,
                10,
                10);

            _productRepositoryMock.GetById(product.Id).Returns(Result<Product>.Success(product));;
            _productRepositoryMock.UnitOfWork.Commit().Returns(true);

            // Act
            var result = await _stockService.DebitStock(product.Id, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(product, result.Value);
            await _productRepositoryMock.Received(1).Update(product);
            await _productRepositoryMock.UnitOfWork.Received(1).Commit();
        }

        [Fact]
        public async Task DebitStock_ShouldPublishLowStockEvent_WhenStockBelowThreshold()
        {
            // Arrange
            var product = Product.CreateProductWithoutCategory(
                "Product name",
                "Product description",
                true,
                19.99m,
                10,
                "image-hash",
                10,
                10, 10);

            _productRepositoryMock.GetById(product.Id).Returns(Result<Product>.Success(product));
            _productRepositoryMock.UnitOfWork.Commit().Returns(true);

            // Act
            var result = await _stockService.DebitStock(product.Id, 1);

            // Assert
            Assert.True(result.IsSuccess);
            await _mediatRHandlerMock.Received(1).PublishEvent(Arg.Is<ProductBelowStockEvent<ProductId>>(e => e.AggregateId == product.Id));
        }
    }
}