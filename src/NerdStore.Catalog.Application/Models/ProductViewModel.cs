using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Models
{
    public record ProductViewModel
    {
        private ProductViewModel() { }
        
        public ProductViewModel(
            ProductId id,
            Name name,
            Description description,
            bool active,
            Price price,
            RegisterDate registerDate,
            Image image,
            StockQuantity stockQuantity,
            Height height,
            Width width,
            Depth depth,
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
        public Name Name { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public Description Description { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public bool Active { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public Price Price { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public RegisterDate RegisterDate { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public Image Image { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public StockQuantity StockQuantity { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public Height Height { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public Width Width { get; init; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must have a minimum value of {1}")]
        public Depth Depth { get; init; }

        public CategoryViewModel Category { get; init; }
    }

    public record ProductId(Guid Value);
    public record Name(string Value);
    public record Description(string Value);
    public record Price(decimal Value);
    public record RegisterDate(DateTime Value);
    public record Image(string Value);
    public record StockQuantity(int Value);
    public record Height(int Value);
    public record Width(int Value);
    public record Depth(int Value);
}