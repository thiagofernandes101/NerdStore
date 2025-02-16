using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Dtos
{
    public record ProductDto
    {
        public ProductDto(
            ProductDtoId id,
            ProductDtoCategoryId categoryId,
            ProductDtoName name,
            ProductDtoDescription description,
            bool active,
            ProductDtoPrice price,
            ProductDtoRegisterDate registerDate,
            ProductDtoImage image,
            ProductDtoStockQuantity stockQuantity,
            ProductDtoHeight height,
            ProductDtoWidth width,
            ProductDtoDepth depth
        )
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            RegisterDate = registerDate;
            Image = image;
            StockQuantity = stockQuantity;
            Height = height;
            Width = width;
            Depth = depth;
        }

        [Key]
        public ProductDtoId Id { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductDtoCategoryId CategoryId { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductDtoName Name { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductDtoDescription Description { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public bool Active { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductDtoPrice Price { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductDtoRegisterDate RegisterDate { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductDtoImage Image { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductDtoStockQuantity StockQuantity { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductDtoHeight Height { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductDtoWidth Width { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductDtoDepth Depth { get; init; }
    }

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