using API.Domain.Features.Storage.Images;
using API.Infrastructure.Common.Repositories;
using API.Infrastructure.Context;

namespace API.Infrastructure.Features.Storage.Images
{
    internal class ImageRepository(AppDbContext context) 
        : Repository<Image>(context), IImageRepository
    {
        private readonly AppDbContext _context = context;
    }
}
