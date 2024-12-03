using Backend.DTO;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class ProductService : IProductoService
    {
        IUnidadDeTrabajo _unidad;

        public ProductService(IUnidadDeTrabajo unidadDeTrabajo)
        {
             this._unidad = unidadDeTrabajo;
        }

        ProductoDTO Convertir (Producto producto)
        {
            return new ProductoDTO
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                Imagen = producto.Imagen,
                Talla = producto.Talla
            };
        }

        Producto Convertir (ProductoDTO producto)
        {
            return new Producto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                Imagen = producto.Imagen,
                Talla = producto.Talla
            };
        }
        public bool Add(ProductoDTO producto)
        {


            try
            {
                Producto entity = Convertir(producto);
                _unidad.ProductoDAL.Add(entity);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar el usuario a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public bool Delete(ProductoDTO producto)
        {

            try
            {
                Producto entity = Convertir(producto);
                _unidad.ProductoDAL.Remove(entity);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el usuario a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public ProductoDTO GetById(int id)
        {
           var producto = _unidad.ProductoDAL.Get(id);
            return Convertir(producto);
        }

        public List<ProductoDTO> GetProductos()
        {
            var productos = _unidad.ProductoDAL.GetAll();
            List<ProductoDTO> productsList = new List<ProductoDTO>();
            foreach (var producto in productos)
            {
                productsList.Add(Convertir(producto));
            }
            return productsList;
        }

        public bool Update(ProductoDTO producto)
        {
            /*try
            {
                _unidad.ProductoDAL.Update(Convertir(producto));
                return producto;
            }
            catch (Exception)
            {
                throw;
            }*/

            try
            {
                var entityExistente = _unidad.ProductoDAL.Get(producto.IdProducto);
                if (entityExistente == null)
                {
                    throw new Exception("Producto no encontrado.");
                }
                entityExistente.NombreProducto = producto.NombreProducto;
                entityExistente.Descripcion = producto.Descripcion;
                entityExistente.Precio = producto.Precio;
                entityExistente.Stock = producto.Stock;
                entityExistente.CategoriaId = producto.CategoriaId;
                entityExistente.Imagen = producto.Imagen;
                entityExistente.Talla = producto.Talla;

                _unidad.ProductoDAL.Update(entityExistente);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al editar el producto.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }
    }
}
