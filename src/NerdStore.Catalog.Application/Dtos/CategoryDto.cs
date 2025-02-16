using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Dtos
{
    public record CategoryDto
    {
        public CategoryDto(CategoryDtoId id, CategoryDtoName name, CategoryDtoCode code)
        {
            Id = id;
            Name = name;
            Cod = code;
        }

        [Key]
        public CategoryDtoId Id { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public CategoryDtoName Name { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public CategoryDtoCode Cod { get; init; }
    }


    public record CategoryDtoId(Guid Value);
    public record CategoryDtoName(string Value);
    public record CategoryDtoCode(int Value);
}
