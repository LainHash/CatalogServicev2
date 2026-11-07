using API.Application.Features.Business;
using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Storage;
using API.Domain.Features.Storage.Images;
using AutoMapper;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Storage
{
    internal class ImageSheetImporter
        : BaseSheetImporter<ImageRow>
    {
        private readonly IImageRepository _imageRepository;

        private readonly IMapper _mapper;

        public ImageSheetImporter(
            IImageRepository imageRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public override string SheetName => "Images";

        protected async override Task ProcessRowAsync(
            ImageRow row,
            int rowIndex,
            ImportResultResponse result,
            CancellationToken cancellationToken)
        {
            var image = _mapper.Map<Image>(row);
            _imageRepository.Add(image);

            result.SuccessCount++;
        }
    }
}
