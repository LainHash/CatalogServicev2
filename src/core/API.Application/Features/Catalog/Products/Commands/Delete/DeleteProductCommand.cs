using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
