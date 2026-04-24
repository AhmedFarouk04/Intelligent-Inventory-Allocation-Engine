using InventoryWarehouseSystem.Application.DTOs.Products;
using InventoryWarehouseSystem.Application.DTOs.Responses;
using InventoryWarehouseSystem.Application.Mappings;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using InventoryWarehouseSystem.SharedKernel.Specifications;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.Products;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ApiPagedResponse<ProductDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    private sealed class ProductPagedSpec : Specification<Product>
    {
        public ProductPagedSpec(int page, int pageSize)
        {
            AddOrderBy(x => x.Name);
            ApplyPaging((page - 1) * pageSize, pageSize);
        }
    }

    public GetProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiPagedResponse<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var page = Math.Max(request.Page, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var spec = new ProductPagedSpec(page, pageSize);
        var items = await _unitOfWork.Products.ListBySpecAsync(spec, cancellationToken);
        var total = await _unitOfWork.Products.CountAsync(new Specification<Product>(), cancellationToken);

        return new ApiPagedResponse<ProductDTO>
        {
            Success = true,
            Data = items.Select(x => x.ToDto()).ToArray(),
            Page = page,
            PageSize = pageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)pageSize)
        };
    }
}
