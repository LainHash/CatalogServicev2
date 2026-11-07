using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Restore
{
    public record RestoreBrandCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
