using NerdStore.Catalog.Domain.Validations;

namespace NerdStore.Catalog.Domain
{
    public record Height(decimal Value);
    public record Width(decimal Value);
    public record Depth(decimal Value);

    public class Dimension
    {
        public Height Height { get; private set; }
        public Width Width { get; private set; }
        public Depth Depth { get; private set; }
        
        private static readonly DimensionValidator _dimensionValidator = new();
        
        public Dimension(Height height, Width width, Depth depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
        }
    }
}
