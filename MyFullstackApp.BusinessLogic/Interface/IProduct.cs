using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Product;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface IProduct
{
    List<ProductDto> GetAllProductsAction();
    ProductDto? GetProductByIdAction(int id);
    ResponceMsg ResponceProductUpdateAction(ProductDto product);
    ResponceMsg ResponceProductDeleteAction(int id);
    ResponceMsg ResponceProductCreateAction(ProductDto product);
}
