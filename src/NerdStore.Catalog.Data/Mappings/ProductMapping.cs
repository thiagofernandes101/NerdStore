using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Domain.Entities;

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
                .HasConversion(name => name.Value, value => new ProductName(value))
                .HasColumnType("varchar(250)");

            builder.Property(property => property.Description)
                .IsRequired()
                .HasConversion(description => description.Value, value => new Description(value))
                .HasColumnType("varchar(500)");

            builder.Property(property => property.Image)
                .IsRequired()
                .HasConversion(image => image.Value, value => new ImageHash(value))
                .HasColumnType("varchar(250)");

            builder.Property(property => property.Price)
                .IsRequired()
                .HasConversion(price => price.Value, value => new Price(value))
                .HasColumnType("decimal(10,2)");

            builder.Property(builder => builder.RegisterDate)
                .IsRequired()
                .HasConversion(registerDate => registerDate.Value, value => new RegisterDate(value))
                .HasColumnType("datetime");

            builder.Property(property => property.StockQuantity)
                .IsRequired()
                .HasConversion(stock => stock.Value, value => new Stock(value))
                .HasColumnType("int");

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            builder.OwnsOne(property => property.Dimension, dimensions =>
            {
                dimensions.Property(property => property.Height)
                    .HasColumnName("Height")
                    .HasColumnType("int");

                dimensions.Property(property => property.Width)
                    .HasColumnName("Width")
                    .HasColumnType("int");
                
                dimensions.Property(property => property.Depth)
                    .HasColumnName("Depth")
                    .HasColumnType("int");
            });
        }
    }
}
