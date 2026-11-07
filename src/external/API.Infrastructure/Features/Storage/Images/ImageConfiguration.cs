using API.Domain.Features.Storage.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Features.Storage.Images
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.AltText)
                .HasMaxLength(255);

            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.StoragePath)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
