using AutoMapper;
using NerdStore.Catalog.Application.Models;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainEntityToDtoMappingProfile : Profile
    {
        public DomainEntityToDtoMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
            .ConvertUsing(src => new ProductViewModel(
                new Models.ProductId(src.Id.Value),
                new ProductCategoryId(src.CategoryId.Value),
                new Models.ProductName(src.Name.Value),
                new Models.ProductDescription(src.Description.Value),
                src.Active,
                new Models.ProductPrice(src.Price.Value),
                new Models.ProductRegisterDate(src.RegisterDate.Value),
                new ProductImage(src.Image.Value),
                new Models.ProductStockQuantity(src.StockQuantity.Value),
                new ProductHeight((int)src.Dimension.Height.Value),
                new ProductWidth((int)src.Dimension.Width.Value),
                new ProductDepth((int)src.Dimension.Depth.Value),
                new CategoryModel(
                    new Models.CategoryId(src.CategoryId.Value),
                    new Models.CategoryName(src.Category.Name.Value),
                    new Models.CategoryCode(src.Category.Code.Value))
            ));

            CreateMap<Category, CategoryModel>()
                .ConvertUsing(src => new CategoryModel(
                    new Models.CategoryId(src.Id.Value),
                    new Models.CategoryName(src.Name.Value),
                    new Models.CategoryCode(src.Code.Value)
                ));
        }
    }
}
