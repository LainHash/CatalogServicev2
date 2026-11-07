using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using API.Domain.Features.Storage.BrandImages;

namespace API.Infrastructure.Features.Storage.BrandImages
{
    internal class BrandImageConfiguration
        : IEntityTypeConfiguration<BrandImage>
    {
        public void Configure(EntityTypeBuilder<BrandImage> builder)
        {
            builder.ToTable("BrandImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.HasOne(x => x.Image)
                .WithOne(x => x.BrandImage)
                .HasForeignKey<BrandImage>(x => x.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Brand)
                .WithMany(x => x.BrandImages)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.BrandId)
                    .IsUnique()
                    .HasFilter("\"IsPrimary\" = true");
        }
    }
}
