using API.Application.Features.Catalog.Units.Commands.Create;
using API.Application.Features.Catalog.Units.Commands.Update;
using API.Application.Features.Catalog.Units.Queries.GetById;
using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Units;

namespace API.Application.Features.Catalog.Units
{
    public interface IUnitService
    {
        Task<PaginationResult<IEnumerable<UnitResponse>>> GetAllAsync(
            ISpecification<Unit> specification,
            CancellationToken cancellationToken);

        Task<Result<UnitDetailResponse>> GetByIdAsync(
            GetUnitByIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<UnitResponse>> CreateAsync(
            CreateUnitCommand command,
            CancellationToken cancellationToken);

        Task<Result<UnitResponse>> UpdateAsync(
            UpdateUnitCommand command,
            UpdateUnitSpecification specification,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            ISpecification<Unit> specification,
            CancellationToken cancellationToken);

        Task<Result> RestoreAsync(
            ISpecification<Unit> specification,
            CancellationToken cancellationToken);
    }
}
