using AutoMapper;
using NerdStore.Catalog.Application.Models;
using NerdStore.Catalog.Domain.ValueObjects;
using Entity = NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DtoToDomainEntityMappingProfile : Profile
    {
        public DtoToDomainEntityMappingProfile()
        {
            CreateMap<ProductViewModel, Entity.Product>()
                .ConstructUsing(_ => new Entity.Product())
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Entity.ProductId.CreateFrom(src.Id.Value)))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => Domain.ValueObjects.Name.Create(src.Name.Value)))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => Domain.ValueObjects.Description.Create(src.Description.Value)))
                .ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(src => src.Active))
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => Domain.ValueObjects.Price.Create(src.Price.Value)))
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId.Value))
                .ForMember(
                    dest => dest.RegisterDate,
                    opt => opt.MapFrom(src => Domain.ValueObjects.RegisterDate.Create(src.RegisterDate.Value)))
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => Image.CreateFromHash(src.Image.Value)))
                .ForMember(
                    dest => dest.Stock,
                    opt => opt.MapFrom(src => Domain.ValueObjects.Stock.Create(src.StockQuantity.Value)))
                .ForMember(
                    dest => dest.Dimension,
                    opt => opt.MapFrom(src => Dimension.Create(src.Height.Value, src.Width.Value, src.Depth.Value)))
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => Entity.Category.Create(src.Category.Name.Value, src.Category.Code.Value)));

            CreateMap<CategoryModel, Entity.Category>()
                .ConstructUsing(_ => new Entity.Category())
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Entity.CategoryId.CreateFrom(src.Id.Value)))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => Domain.ValueObjects.Name.Create(src.Name.Value)))
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(src => Domain.ValueObjects.CategoryCode.Create(src.Code.Value)))
                .ForMember(
                    dest => dest.Products,
                    opt => opt.Ignore());
        }
    }
}
