using AutoMapper;
using API.Application.Features.Catalog.Units.Commands.Create;
using API.Application.Features.Catalog.Units.Commands.Update;
using API.Application.Features.Catalog.Units.Queries.GetById;
using API.Contract.DTOs.Catalog.Units;
using System.Net;
using API.Domain.Features.Catalog.Units;
using API.Application.Features.Catalog.Units;
using API.Application.Features.Business;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Specifications;
using API.Domain.Common.Models.Messages;

namespace API.Infrastructure.Features.Catalog.Units
{
    internal class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(
            IUnitRepository unitRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<IEnumerable<UnitResponse>>> GetAllAsync(
            ISpecification<Unit> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _unitRepository.CountAsync(specification, cancellationToken);

            var units = await _unitRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<UnitResponse>>(units);
            return PaginationResult<IEnumerable<UnitResponse>>
                .Succeed(response, Success.Retrieved("Unit"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<UnitDetailResponse>> GetByIdAsync(
            GetUnitByIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.FindAsync(specification, cancellationToken);
            if (unit == null)
            {
                return Result<UnitDetailResponse>
                    .Fail(Error.NotFound("Unit"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<UnitDetailResponse>(unit);
            return Result<UnitDetailResponse>
                .Succeed(response, Success.Retrieved("Unit"));
        }

        public async Task<Result<UnitResponse>> CreateAsync(
            CreateUnitCommand command,
            CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if (unit is not null)
            {
                return Result<UnitResponse>
                    .Fail(Error.ExistedName("Unit"), HttpStatusCode.Conflict);
            }

            unit = _mapper.Map<Unit>(command.Body);
            _unitRepository.Add(unit);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<UnitResponse>(unit);
            return Result<UnitResponse>
                .Succeed(response, Success.Created("Unit"), HttpStatusCode.Created);
        }

        public async Task<Result<UnitResponse>> UpdateAsync(
            UpdateUnitCommand command,
            UpdateUnitSpecification specification,
            CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.FindAsync(specification, cancellationToken);
            if (unit is null)
            {
                return Result<UnitResponse>
                    .Fail(Error.NotFound("Unit"), HttpStatusCode.NotFound);
            }

            if (await _unitRepository
                .IsExistingNameAsync(command.Id, command.Body.Name, cancellationToken))
            {
                return Result<UnitResponse>
                    .Fail(Error.ExistedName("Unit"), HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, unit);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<UnitResponse>(unit);
            return Result<UnitResponse>
                .Succeed(response, Success.Updated("Unit"), HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Unit> specification,
            CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.FindAsync(specification, cancellationToken);
            if (unit == null)
            {
                return Result
                    .Fail(Error.NotFound("Unit"), HttpStatusCode.NotFound);
            }

            if (unit.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Unit"), HttpStatusCode.BadRequest);
            }

            unit.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Unit"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Unit> specification,
            CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.FindAsync(specification, cancellationToken);
            if (unit == null)
            {
                return Result
                    .Fail(Error.NotFound("Unit"), HttpStatusCode.NotFound);
            }

            if (!unit.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Unit"), HttpStatusCode.BadRequest);
            }

            unit.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Unit"));
        }
    }
}
