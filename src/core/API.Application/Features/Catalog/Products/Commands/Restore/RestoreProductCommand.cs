using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Products.Commands.Restore
{
    public record RestoreProductCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
