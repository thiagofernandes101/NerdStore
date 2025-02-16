using AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DtoToDomainEntityMappingProfile : Profile
    {
        public DtoToDomainEntityMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(_ => new Product())
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => Domain.Entities.ProductName.Create(src.Name.Value)))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => Domain.Entities.ProductDescription.Create(src.Description.Value)))
                .ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(src => src.Active))
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => Domain.Entities.ProductPrice.Create(src.Price.Value)))
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId.Value))
                .ForMember(
                    dest => dest.RegisterDate,
                    opt => opt.MapFrom(src => Domain.Entities.ProductRegisterDate.Create(src.RegisterDate.Value)))
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => ProductImageHash.Create(src.Image.Value)))
                .ForMember(
                    dest => dest.StockQuantity,
                    opt => opt.MapFrom(src => Domain.Entities.ProductStockQuantity.Create(src.StockQuantity.Value)))
                .ForMember(
                    dest => dest.Dimension,
                    opt => opt.MapFrom(src => ProductDimension.Create(src.Height.Value, src.Width.Value, src.Depth.Value)))
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => Category.Create(src.Category.Name.Value, src.Category.Code.Value)));

            CreateMap<CategoryModel, Category>()
                .ConstructUsing(_ => new Category())
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => Domain.Entities.CategoryName.Create(src.Name.Value)))
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(src => Domain.Entities.CategoryCode.Create(src.Code.Value)))
                .ForMember(
                    dest => dest.Products,
                    opt => opt.Ignore());
        }
    }
}
