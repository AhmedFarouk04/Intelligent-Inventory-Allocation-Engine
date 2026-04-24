using InventoryWarehouseSystem.Application.DTOs.Warehouses;
using InventoryWarehouseSystem.Application.Mappings;
using InventoryWarehouseSystem.Domain.Repositories;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.Warehouses;

public sealed class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, IReadOnlyCollection<WarehouseDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetWarehousesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<WarehouseDTO>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Warehouses.GetAllAsync(cancellationToken);
        return data.Select(x => x.ToDto()).ToArray();
    }
}

