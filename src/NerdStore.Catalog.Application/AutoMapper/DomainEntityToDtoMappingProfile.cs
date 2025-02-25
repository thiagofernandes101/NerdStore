using AutoMapper;
using ApplicationModel = NerdStore.Catalog.Application.Models;
using DomainEntity = NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainEntityToDtoMappingProfile : Profile
    {
        public DomainEntityToDtoMappingProfile()
        {
            CreateMap<DomainEntity.Product, ApplicationModel.ProductViewModel>()
                .ConvertUsing(src => new ApplicationModel.ProductViewModel(
                    new ApplicationModel.ProductId(src.Id.Value),
                    new ApplicationModel.ProductCategoryId(src.CategoryId.Value),
                    new ApplicationModel.ProductName(src.Name.Value),
                    new Models.ProductDescription(src.Description.Value),
                    src.Active,
                    new ApplicationModel.ProductPrice(src.Price.Value),
                    new ApplicationModel.ProductRegisterDate(src.RegisterDate.Value),
                    new ApplicationModel.ProductImage(src.Image.Value),
                    new ApplicationModel.ProductStockQuantity(src.StockQuantity.Value),
                    new ApplicationModel.ProductHeight((int)src.Dimension.Height.Value),
                    new ApplicationModel.ProductWidth((int)src.Dimension.Width.Value),
                    new ApplicationModel.ProductDepth((int)src.Dimension.Depth.Value),
                    src.Category == null ?
                        new ApplicationModel.CategoryModel(
                            new Models.CategoryId(Guid.Empty),
                            new Models.CategoryName(string.Empty),
                            new Models.CategoryCode(0))
                        : new ApplicationModel.CategoryModel(
                            new Models.CategoryId(src.CategoryId.Value),
                            new Models.CategoryName(src.Category.Name.Value),
                            new Models.CategoryCode(src.Category.Code.Value))
                ));

            CreateMap<DomainEntity.Category, ApplicationModel.CategoryModel>()
                .ConvertUsing(src => new ApplicationModel.CategoryModel(
                    new Models.CategoryId(src.Id.Value),
                    new Models.CategoryName(src.Name.Value),
                    new Models.CategoryCode(src.Code.Value)
                ));
        }
    }
}
