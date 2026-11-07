using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using API.Application.Features.Catalog.Categories.Commands.Create;
using API.Application.Features.Catalog.Categories.Commands.Delete;
using API.Application.Features.Catalog.Categories.Commands.Restore;
using API.Application.Features.Catalog.Categories.Commands.Update;
using API.Application.Features.Catalog.Categories.Queries.GetAll;
using API.Application.Features.Catalog.Categories.Queries.GetById;
using API.Contract.DTOs.Catalog.Categories;
using CatalogAPI.Extensions;

namespace CatalogAPI.Controllers.Catalog
{
    [Route("api/catalog/categories")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        [OutputCache(PolicyName = "Short")]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllCategoriesQuery query,
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
            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateCategoryRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateCategoryCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateCategoryRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateCategoryCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreCategoryCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
