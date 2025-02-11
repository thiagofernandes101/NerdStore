using NerdStore.Catalog.Domain.Validations;
using NerdStore.Core;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public record Height(decimal Value)
    {
        public static Height NewHeight(decimal value) => new(value);
    }
    public record Width(decimal Value)
    {
        public static Width NewWidth(decimal value) => new(value);
    }
    public record Depth(decimal Value)
    {
        public static Depth NewDepth(decimal value) => new(value);
    }

    public class Dimension
    {
        public Height Height { get; private set; }
        public Width Width { get; private set; }
        public Depth Depth { get; private set; }

        private static readonly DimensionValidator _dimensionValidator = new();

        private Dimension(Height height, Width width, Depth depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
        }

        public static Dimension NewDimension(decimal height, decimal width, decimal depth)
        {
            var dimension = new Dimension(
                Height.NewHeight(height),
                Width.NewWidth(width),
                Depth.NewDepth(depth)
            );

            return dimension;
        }
    }
}
