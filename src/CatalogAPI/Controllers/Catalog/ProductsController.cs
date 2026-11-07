using API.Application.Features.Catalog.Products.Commands.Create;
using API.Application.Features.Catalog.Products.Commands.Delete;
using API.Application.Features.Catalog.Products.Commands.Restore;
using API.Application.Features.Catalog.Products.Commands.Update;
using API.Application.Features.Catalog.Products.Queries.GetAll;
using API.Application.Features.Catalog.Products.Queries.GetById;
using API.Contract.DTOs.Catalog.Products;
using CatalogAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers.Catalog
{
    [Route("api/catalog/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllProductsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            [FromQuery] Guid? branchId,
            CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(id, branchId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateProductRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreProductCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
