using API.Domain.Features.Storage;
using API.Domain.Features.Storage.ProductImages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Features.Storage.ProductImages
{
    internal class ProductImageConfiguration 
        : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.HasOne(x => x.Image)
                .WithOne(x => x.ProductImage)
                .HasForeignKey<ProductImage>(x => x.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductImages)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ProductId)
                    .IsUnique()
                    .HasFilter("\"IsPrimary\" = true");

        }
    }
}
