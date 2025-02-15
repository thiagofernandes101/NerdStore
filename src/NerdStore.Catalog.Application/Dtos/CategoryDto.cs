namespace NerdStore.Catalog.Application.Dtos
{
    public record CategoryDto(
        CategoryDtoId Id,
        CategoryDtoName Name,
        CategoryDtoCode Code
    );

    public record CategoryDtoId(Guid Value);
    public record CategoryDtoName(string Value);
    public record CategoryDtoCode(int Value);
}
