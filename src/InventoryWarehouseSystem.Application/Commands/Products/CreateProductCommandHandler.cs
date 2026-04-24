using InventoryWarehouseSystem.Application.Mappings;
using InventoryWarehouseSystem.Application.DTOs.Products;
using InventoryWarehouseSystem.Application.DTOs.Responses;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using MediatR;

namespace InventoryWarehouseSystem.Application.Commands.Products;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Products.GetBySkuAsync(request.Sku, cancellationToken);
        if (existing is not null)
        {
            return ApiResponse<ProductDTO>.Failure("Product with same SKU already exists", 409);
        }

        var product = Product.Create(request.Sku, request.Name, request.CategoryId, request.ReorderLevel, request.Description);
        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<ProductDTO>.SuccessResponse(product.ToDto(), "Product created successfully", 201);
    }
}
