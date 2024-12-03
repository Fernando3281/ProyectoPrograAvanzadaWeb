using FrontEnd.Models;
namespace FrontEnd.Helpers.Interfaces
{
    public interface IProductoHelper
    {
        List<ProductoViewModel> GetAll();
        ProductoViewModel GetById(int id);
        ProductoViewModel AddProduct(ProductoViewModel productoViewModel);

        ProductoViewModel EditProduct(ProductoViewModel productoViewModel);

        void DeleteProduct(int id);
    }
}
