using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using API.Application.Features.Catalog.Brands.Commands.Create;
using API.Application.Features.Catalog.Brands.Commands.Delete;
using API.Application.Features.Catalog.Brands.Commands.Restore;
using API.Application.Features.Catalog.Brands.Commands.Update;
using API.Application.Features.Catalog.Brands.Queries.GetAll;
using API.Application.Features.Catalog.Brands.Queries.GetById;
using API.Contract.DTOs.Catalog.Brands;
using CatalogAPI.Extensions;

namespace CatalogAPI.Controllers.Catalog
{
    [Route("api/catalog/brands")]
    [ApiController]
    public class BrandsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        [OutputCache(PolicyName = "Short")]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllBrandsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetBrandByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateBrandRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateBrandCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateBrandRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateBrandCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteBrandCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreBrandCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
