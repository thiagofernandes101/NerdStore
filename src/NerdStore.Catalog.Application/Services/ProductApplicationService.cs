using AutoMapper;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Catalog.Domain.Services;
using ApplicationModel = NerdStore.Catalog.Application.Models;
using Entity = NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.Services
{
    public interface IProductApplicationService
    {
        Task<IEnumerable<ApplicationModel.ProductViewModel>> GetByCategory(ApplicationModel.CategoryCode code);
        Task<ApplicationModel.ProductViewModel> GetById(ApplicationModel.ProductId id);
        Task<IEnumerable<ApplicationModel.ProductViewModel>> GetAll();
        Task<IEnumerable<ApplicationModel.ProductViewModel>> GetCategories();

        Task AddProduct(ApplicationModel.ProductViewModel productDto);
        Task UpdateProduct(ApplicationModel.ProductViewModel productDto);

        Task<ApplicationModel.ProductViewModel> DebitStock(ApplicationModel.ProductId id, int quantity);
        Task<ApplicationModel.ProductViewModel> ReplenishStock(ApplicationModel.ProductId id, int quantity);
    }

    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductApplicationService(IProductRepository productRepository, IStockService stockService, IMapper mapper)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
        }

        public async Task AddProduct(ApplicationModel.ProductViewModel productDto)
        {
            var mappedProduct = _mapper.Map<Entity.Product>(productDto);
            await _productRepository.Add(mappedProduct);
            await _productRepository.UnitOfWork.Commit();
        }

        public Task<ApplicationModel.ProductViewModel> DebitStock(ApplicationModel.ProductId id, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationModel.ProductViewModel>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ApplicationModel.ProductViewModel>>(products);
        }

        public async Task<IEnumerable<ApplicationModel.ProductViewModel>> GetByCategory(ApplicationModel.CategoryCode code)
        {
            var mappedCode = _mapper.Map<Entity.CategoryCode>(code);
            var products = await _productRepository.GetByCategory(mappedCode);
            return _mapper.Map<IEnumerable<ApplicationModel.ProductViewModel>>(products);
        }

        public async Task<ApplicationModel.ProductViewModel> GetById(ApplicationModel.ProductId id)
        {
            var mappedProductId = _mapper.Map<Entity.ProductId>(id);
            var product = await _productRepository.GetById(mappedProductId);
            return _mapper.Map<ApplicationModel.ProductViewModel>(product);
        }

        public async Task<IEnumerable<ApplicationModel.ProductViewModel>> GetCategories()
        {
            var categories = await _productRepository.GetCategories();
            return _mapper.Map<IEnumerable<ApplicationModel.ProductViewModel>>(categories);
        }

        public Task<ApplicationModel.ProductViewModel> ReplenishStock(ApplicationModel.ProductId id, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProduct(ApplicationModel.ProductViewModel productDto)
        {
            var product = _mapper.Map<Entity.Product>(productDto);
            await _productRepository.Update(product);
            await _productRepository.UnitOfWork.Commit();
        }
    }
}
