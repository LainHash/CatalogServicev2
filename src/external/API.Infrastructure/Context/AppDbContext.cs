using API.Application.Features.Business.Audit;
using API.Contract.Settings;
using API.Domain.Features.Business.Audit;
using API.Domain.Features.Business.Configurations;
using API.Domain.Features.Catalog.Brands;
using API.Domain.Features.Catalog.Categories;
using API.Domain.Features.Catalog.Products;
using API.Domain.Features.Catalog.Units;
using API.Domain.Features.Storage.BrandImages;
using API.Domain.Features.Storage.Images;
using API.Domain.Features.Storage.ProductImages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Infrastructure.Context
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<SystemSetting> SystemSettings { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Unit> Units { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<BrandImage> BrandImages { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;

        // ── IAuditContext injected via constructor ────────────────────────────
        private readonly IAuditContext _auditContext;
        private readonly AuditLogSettings _auditLogSettings;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IAuditContext auditContext,
            IOptions<AppSettings> settings)
            : base(options)
        {
            _auditContext = auditContext;
            _auditLogSettings = settings.Value.AuditLog;
        }
    }
}
