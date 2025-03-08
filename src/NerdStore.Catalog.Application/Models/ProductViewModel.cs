using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Models
{
    public record ProductViewModel
    {
        private ProductViewModel() { }
        
        public ProductViewModel(
            ProductId id,
            ProductName name,
            ProductDescription description,
            bool active,
            ProductPrice price,
            ProductRegisterDate registerDate,
            ProductImage image,
            ProductStockQuantity stockQuantity,
            ProductHeight height,
            ProductWidth width,
            ProductDepth depth,
            CategoryViewModel category
        )
        {
            Id = id;
            CategoryId = category.Id;
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
            Category = category;
        }

        [Key]
        public ProductId Id { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public CategoryId CategoryId { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductName Name { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductDescription Description { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public bool Active { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductPrice Price { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductRegisterDate RegisterDate { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public ProductImage Image { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductStockQuantity StockQuantity { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductHeight Height { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductWidth Width { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public ProductDepth Depth { get; init; }

        public CategoryViewModel Category { get; init; }
    }

    public record ProductId(Guid Value);
    public record ProductName(string Value);
    public record ProductDescription(string Value);
    public record ProductPrice(decimal Value);
    public record ProductRegisterDate(DateTime Value);
    public record ProductImage(string Value);
    public record ProductStockQuantity(int Value);
    public record ProductHeight(int Value);
    public record ProductWidth(int Value);
    public record ProductDepth(int Value);
}