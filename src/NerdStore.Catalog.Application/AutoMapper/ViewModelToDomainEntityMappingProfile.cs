using AutoMapper;
using ApplicationModel = NerdStore.Catalog.Application.Models;
using DomainEntity = NerdStore.Catalog.Domain.Entities;
using DomainValueObject = NerdStore.Catalog.Domain.ValueObjects;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainEntityMappingProfile : Profile
    {
        public ViewModelToDomainEntityMappingProfile()
        {
            CreateMap<ApplicationModel.ProductId, DomainEntity.ProductId>()
                .ConstructUsing(src => DomainEntity.ProductId.CreateFrom(src.Value));

            CreateMap<ApplicationModel.CategoryId, DomainEntity.CategoryId>()
                .ConstructUsing(src => DomainEntity.CategoryId.CreateFrom(src.Value));

            CreateMap<ApplicationModel.ProductName, DomainValueObject.Name>()
                .ConstructUsing(src => DomainValueObject.Name.Create(src.Value));

            CreateMap<ApplicationModel.CategoryCode, DomainValueObject.CategoryCode>()
                .ConstructUsing(src => DomainValueObject.CategoryCode.Create(src.Value));

            CreateMap<ApplicationModel.CategoryName, DomainValueObject.Name>()
                .ConstructUsing(src => DomainValueObject.Name.Create(src.Value));

            CreateMap<ApplicationModel.ProductViewModel, DomainEntity.Product>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => DomainEntity.ProductId.CreateFrom(src.Id.Value)))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => DomainValueObject.Name.Create(src.Name.Value)))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => DomainValueObject.Description.Create(src.Description.Value)))
                .ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(src => src.Active))
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => DomainValueObject.Price.Create(src.Price.Value)))
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId.Value))
                .ForMember(
                    dest => dest.RegisterDate,
                    opt => opt.MapFrom(src => DomainValueObject.RegisterDate.Create(src.RegisterDate.Value)))
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => DomainValueObject.Image.CreateFromHash(src.Image.Value)))
                .ForMember(
                    dest => dest.Stock,
                    opt => opt.MapFrom(src => DomainValueObject.Stock.Create(src.StockQuantity.Value)))
                .ForMember(
                    dest => dest.Dimension,
                    opt => opt.MapFrom(src => DomainValueObject.Dimension.Create(src.Height.Value, src.Width.Value, src.Depth.Value)))
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => DomainEntity.Category.Create(src.Category.Name.Value, src.Category.Code.Value)));

            CreateMap<ApplicationModel.CategoryViewModel, DomainEntity.Category>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => DomainEntity.CategoryId.CreateFrom(src.Id.Value)))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => DomainValueObject.Name.Create(src.Name.Value)))
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(src => DomainValueObject.CategoryCode.Create(src.Code.Value)))
                .ForMember(
                    dest => dest.Products,
                    opt => opt.Ignore());
        }
    }
}
