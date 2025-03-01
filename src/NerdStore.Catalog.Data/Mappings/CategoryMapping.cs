using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.ValueObjects;

namespace NerdStore.Catalog.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(property => property.Id);

            builder.Property(property => property.Id)
                .HasConversion(id => id.Value, id => new CategoryId(id))
                .IsRequired();

            builder.Property(propertyExpression => propertyExpression.Name)
                .IsRequired()
                .HasConversion(name => name.Value, value => Name.Create(value))
                .HasColumnType("varchar(250)");

            builder.Property(property => property.Code)
                .IsRequired()
                .HasConversion(code => code.Value, value => CategoryCode.Create(value))
                .HasColumnType("int");

            builder.HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId);
        }
    }
}
