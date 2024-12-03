using Backend.DTO;

namespace Backend.Services.Interfaces
{
    public interface IProductoService
    {
        List<ProductoDTO> GetProductos();
        bool Add(ProductoDTO producto);
        bool Update(ProductoDTO producto);
        bool Delete(ProductoDTO productoDTO);

        ProductoDTO GetById(int id);
    }
}
