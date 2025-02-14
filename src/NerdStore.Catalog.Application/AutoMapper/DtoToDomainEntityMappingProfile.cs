using AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DtoToDomainEntityMappingProfile : Profile
    {
        public DtoToDomainEntityMappingProfile() 
        {
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => ProductName.NewProductName(src.Name.Value)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => Description.NewDescription(src.Description.Value)))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Price.NewPrice(src.Price.Value)))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId.Value))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => RegisterDate.NewRegisterDate(src.RegisterDate.Value)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ImageHash.NewImageHash(src.Image.Value)))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => StockQuantity.NewStock(src.StockQuantity.Value)))
                .ForMember(dest => dest.Dimension, opt => opt.MapFrom(src => Dimension.NewDimension(src.Height.Value, src.Width.Value, src.Depth.Value)));
        }
    }
}
