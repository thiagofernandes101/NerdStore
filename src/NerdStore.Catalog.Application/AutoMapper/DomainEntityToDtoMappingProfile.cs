using AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainEntityToDtoMappingProfile : Profile
    {
        public DomainEntityToDtoMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
            .ConvertUsing(src => new ProductViewModel(
                new Dtos.ProductId(src.Id.Value),
                new ProductCategoryId(src.CategoryId.Value),
                new Dtos.ProductName(src.Name.Value),
                new Dtos.ProductDescription(src.Description.Value),
                src.Active,
                new Dtos.ProductPrice(src.Price.Value),
                new Dtos.ProductRegisterDate(src.RegisterDate.Value),
                new ProductImage(src.Image.Value),
                new Dtos.ProductStockQuantity(src.StockQuantity.Value),
                new ProductHeight((int)src.Dimension.Height.Value),
                new ProductWidth((int)src.Dimension.Width.Value),
                new ProductDepth((int)src.Dimension.Depth.Value),
                new CategoryModel(
                    new Dtos.CategoryId(src.CategoryId.Value),
                    new Dtos.CategoryName(src.Category.Name.Value),
                    new Dtos.CategoryCode(src.Category.Code.Value))
            ));

            CreateMap<Category, CategoryModel>()
                .ConvertUsing(src => new CategoryModel(
                    new Dtos.CategoryId(src.Id.Value),
                    new Dtos.CategoryName(src.Name.Value),
                    new Dtos.CategoryCode(src.Code.Value)
                ));
        }
    }
}
