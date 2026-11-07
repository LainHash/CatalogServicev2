using API.Application.Features.Business.Audit.Queries.GetAll;
using CatalogAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers.Business.Audit
{
    [Route("api/admin/catalog/audit-logs")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AuditLogsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllAuditLogsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
