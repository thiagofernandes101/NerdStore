using AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainEntityToDtoMappingProfile : Profile
    {
        public DomainEntityToDtoMappingProfile()
        {
            CreateMap<Product, ProductDto>()
            .ConvertUsing(src => new ProductDto(
                new ProductDtoId(src.Id.Value),
                new ProductDtoCategoryId(src.CategoryId.Value),
                new ProductDtoName(src.Name.Value),
                new ProductDtoDescription(src.Description.Value),
                src.Active,
                new ProductDtoPrice(src.Price.Value),
                new ProductDtoRegisterDate(src.RegisterDate.Value),
                new ProductDtoImage(src.Image.Value),
                new ProductDtoStockQuantity(src.StockQuantity.Value),
                new ProductDtoHeight((int)src.Dimension.Height.Value),
                new ProductDtoWidth((int)src.Dimension.Width.Value),
                new ProductDtoDepth((int)src.Dimension.Depth.Value),
                new CategoryDto(
                    new CategoryDtoId(src.CategoryId.Value),
                    new CategoryDtoName(src.Category.Name.Value),
                    new CategoryDtoCode(src.Category.Code.Value))
            ));

            CreateMap<Category, CategoryDto>()
                .ConvertUsing(src => new CategoryDto(
                    new CategoryDtoId(src.Id.Value),
                    new CategoryDtoName(src.Name.Value),
                    new CategoryDtoCode(src.Code.Value)
                ));
        }
    }
}
