using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Delete
{
    public record DeleteBrandCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
