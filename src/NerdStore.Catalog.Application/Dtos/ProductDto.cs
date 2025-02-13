namespace NerdStore.Catalog.Application.Dtos
{
    public record ProductDto(
        ProductDtoId Id,
        ProductDtoCategoryId CategoryId,
        ProductDtoName Name,
        ProductDtoDescription Description,
        bool Active,
        ProductDtoPrice Price,
        ProductDtoRegisterDate RegisterDate,
        ProductDtoImage Image,
        ProductDtoStockQuantity StockQuantity,
        ProductDtoHeight Height,
        ProductDtoWidth Width,
        ProductDtoDepth Depth
    );

    public record ProductDtoId(Guid Value);
    public record ProductDtoCategoryId(Guid Value);
    public record ProductDtoName(string Value);
    public record ProductDtoDescription(string Value);
    public record ProductDtoPrice(decimal Value);
    public record ProductDtoRegisterDate(DateTime Value);
    public record ProductDtoImage(string Value);
    public record ProductDtoStockQuantity(int Value);
    public record ProductDtoHeight(int Value);
    public record ProductDtoWidth(int Value);
    public record ProductDtoDepth(int Value);
}