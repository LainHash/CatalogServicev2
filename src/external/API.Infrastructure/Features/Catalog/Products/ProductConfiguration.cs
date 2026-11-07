using API.Domain.Features.Catalog.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Features.Catalog.Products
{
    internal class ProductConfiguration
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.PublicId)
                .IsRequired();
            builder.HasIndex(x => x.PublicId)
                .IsUnique();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.InventoryType)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
