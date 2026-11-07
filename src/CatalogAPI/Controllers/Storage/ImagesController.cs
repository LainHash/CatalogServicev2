using API.Application.Features.Storage.Images.Commands.UploadBrandImage;
using API.Application.Features.Storage.Images.Commands.UploadProductImage;
using API.Application.Features.Storage.Images.Queries;
using API.Contract.DTOs.Storage.BrandImages;
using API.Contract.DTOs.Storage.ProductImages;
using CatalogAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers.Storage
{
    [Route("api/storage/images")]
    [ApiController]
    public class ImagesController(IMediator mediator) 
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllImagesQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager")]
        [HttpPost("products/{productId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadProductImage(
            [FromRoute] Guid productId,
            IFormFile file,
            [FromForm] UploadProductImageRequest metadata,
            CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File ảnh không được để trống.");

            await using var stream = file.OpenReadStream();

            var command = new UploadProductImageCommand(productId, stream, file.FileName, metadata);

            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager")]
        [HttpPost("brands/{brandId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadBrandImage(
            [FromRoute] Guid brandId,
            IFormFile file,
            [FromForm] UploadBrandImageRequest metadata,
            CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File ảnh không được để trống.");

            await using var stream = file.OpenReadStream();

            var command = new UploadBrandImageCommand(brandId, stream, file.FileName, metadata);

            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
