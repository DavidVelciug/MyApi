using AutoMapper;
using MyFullstackApp.BusinessLogic.Core.Products;
using MyFullstackApp.BusinessLogic.Interface;
using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Product;

namespace MyFullstackApp.BusinessLogic.Functions.Products;

public class ProductFlow : ProductAction, IProduct
{
    public ProductFlow(IMapper mapper) : base(mapper) { }

    public List<ProductDto> GetAllProductsAction()
    {
        return ExecuteGetAllProductsAction();
    }

    public ProductDto? GetProductByIdAction(int id)
    {
        return GetProductDataByIdAction(id);
    }

    public ResponceMsg ResponceProductUpdateAction(ProductDto product)
    {
        return ExecuteProductUpdateAction(product);
    }

    public ResponceMsg ResponceProductDeleteAction(int id)
    {
        return ExecuteProductDeleteAction(id);
    }

    public ResponceMsg ResponceProductCreateAction(ProductDto product)
    {
        return ExecuteProductCreateAction(product);
    }
}
