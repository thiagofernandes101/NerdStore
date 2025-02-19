using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;

namespace NerdStore.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(property => property.Id);

            builder.Property(property => property.Id)
                .HasConversion(id => id.Value, id => new ProductId(id))
                .IsRequired();

            builder.Property(property => property.Name)
                .IsRequired()
                .HasConversion(name => name.Value, value => ProductName.Create(value))
                .HasColumnType("varchar(250)");

            builder.Property(property => property.Description)
                .IsRequired()
                .HasConversion(description => description.Value, value => ProductDescription.Create(value))
                .HasColumnType("varchar(500)");

            builder.Property(property => property.Image)
                .IsRequired()
                .HasConversion(image => image.Value, value => ProductImageHash.Create(value))
                .HasColumnType("varchar(250)");

            builder.Property(property => property.Price)
                .IsRequired()
                .HasConversion(price => price.Value, value => ProductPrice.Create(value))
                .HasColumnType("decimal(10,2)");

            builder.Property(builder => builder.RegisterDate)
                .IsRequired()
                .HasConversion(registerDate => registerDate.Value, value => ProductRegisterDate.Create(value))
                .HasColumnType("datetime");

            builder.Property(property => property.StockQuantity)
                .IsRequired()
                .HasConversion(stock => stock.Value, value => ProductStockQuantity.Create(value))
                .HasColumnType("int");

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            builder.OwnsOne(property => property.Dimension, dimensions =>
            {
                dimensions.Property(property => property.Height)
                    .HasColumnName("Height")
                    .HasConversion(height => height.Value, value => new Height(value))
                    .HasColumnType("int");

                dimensions.Property(property => property.Width)
                    .HasColumnName("Width")
                    .HasConversion(width => width.Value, value => new Width(value))
                    .HasColumnType("int");

                dimensions.Property(property => property.Depth)
                    .HasColumnName("Depth")
                    .HasConversion(depth => depth.Value, value => new Depth(value))
                    .HasColumnType("int");
            });
        }
    }
}
