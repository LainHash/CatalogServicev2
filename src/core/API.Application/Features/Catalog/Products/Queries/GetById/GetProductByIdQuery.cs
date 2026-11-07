using MediatR;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Products.Queries.GetById
{
    public record GetProductByIdQuery(Guid Id, Guid? BranchId = null) 
        : IRequest<Result<ProductDetailResponse>>
    {
    }
}
