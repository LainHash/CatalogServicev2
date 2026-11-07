using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using API.Application.Features.Catalog.Units.Commands.Create;
using API.Application.Features.Catalog.Units.Commands.Delete;
using API.Application.Features.Catalog.Units.Commands.Restore;
using API.Application.Features.Catalog.Units.Commands.Update;
using API.Application.Features.Catalog.Units.Queries.GetAll;
using API.Application.Features.Catalog.Units.Queries.GetById;
using API.Contract.DTOs.Catalog.Units;
using CatalogAPI.Extensions;

namespace CatalogAPI.Controllers.Catalog
{
    [Route("api/catalog/units")]
    [ApiController]
    public class UnitsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        [OutputCache(PolicyName = "Short")]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllUnitsQuery query,
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
            var query = new GetUnitByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateUnitRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateUnitCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateUnitRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateUnitCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteUnitCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreUnitCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
