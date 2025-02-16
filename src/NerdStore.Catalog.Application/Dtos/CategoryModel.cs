using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Dtos
{
    public record CategoryModel
    {
        public CategoryModel(CategoryId id, CategoryName name, CategoryCode code)
        {
            Id = id;
            Name = name;
            Code = code;
        }

        [Key]
        public CategoryId Id { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public CategoryName Name { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public CategoryCode Code { get; init; }
    }


    public record CategoryId(Guid Value);
    public record CategoryName(string Value);
    public record CategoryCode(int Value);
}
