using MediatR;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, UpdateProductRequest Body)
        : IRequest<Result<ProductResponse>>
    {
    }
}
