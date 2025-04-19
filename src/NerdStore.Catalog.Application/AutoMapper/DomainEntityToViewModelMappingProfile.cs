using AutoMapper;
using ApplicationModel = NerdStore.Catalog.Application.Models;
using Domain = NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainEntityToDtoMappingProfile : Profile
    {
        public DomainEntityToDtoMappingProfile()
        {
            CreateMap<Domain.Entities.Product, ApplicationModel.ProductViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => new ApplicationModel.ProductId(src.Id.Value))
                )
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => new ApplicationModel.CategoryId(src.CategoryId.Value))
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => new ApplicationModel.Name(src.Name.Value))
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => new ApplicationModel.Description(src.Description.Value))
                )
                .ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(src => src.Active)
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => new ApplicationModel.Price(src.Price.Value))
                )
                .ForMember(
                    dest => dest.RegisterDate,
                    opt => opt.MapFrom(src => new ApplicationModel.RegisterDate(src.RegisterDate.Value))
                )
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => new ApplicationModel.Image(src.Image.Value))
                )
                .ForMember(
                    dest => dest.StockQuantity,
                    opt => opt.MapFrom(src => new ApplicationModel.StockQuantity(src.Stock.Amount))
                )
                .ForMember(
                    dest => dest.Height,
                    opt => opt.MapFrom(src => new ApplicationModel.Height((int)src.Dimension.Height.Value))
                )
                .ForMember(
                    dest => dest.Width,
                    opt => opt.MapFrom(src => new ApplicationModel.Width((int)src.Dimension.Width.Value))
                )
                .ForMember(
                    dest => dest.Depth,
                    opt => opt.MapFrom(src => new ApplicationModel.Depth((int)src.Dimension.Depth.Value))
                )
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => new ApplicationModel.CategoryViewModel(
                        new ApplicationModel.CategoryId(src.Category.Id.Value),
                        new ApplicationModel.CategoryName(src.Category.Name.Value),
                        new ApplicationModel.CategoryCode(src.Category.Code.Value))
                    )
                );

            CreateMap<Domain.Entities.CategoryId, ApplicationModel.CategoryId>()
                .ConstructUsing(src => new ApplicationModel.CategoryId(src.Value));

            CreateMap<Domain.ValueObjects.Name, ApplicationModel.CategoryName>()
                .ConstructUsing(src => new ApplicationModel.CategoryName(src.Value));
            
            CreateMap<Domain.ValueObjects.CategoryCode, ApplicationModel.CategoryCode>()
                .ConstructUsing(src => new ApplicationModel.CategoryCode(src.Value));

            CreateMap<Domain.Entities.Category, ApplicationModel.CategoryViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => new ApplicationModel.CategoryId(src.Id.Value))
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => new ApplicationModel.CategoryName(src.Name.Value))
                )
                .ForMember(
                    dest => dest.Code,
                    opt => opt.MapFrom(src => new ApplicationModel.CategoryCode(src.Code.Value))
                );
        }
    }
}
