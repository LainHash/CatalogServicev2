using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Products.Commands.Create
{
    public record CreateProductCommand(CreateProductRequest Body)
        : IRequest<Result<ProductResponse>>
    {
    }
}
