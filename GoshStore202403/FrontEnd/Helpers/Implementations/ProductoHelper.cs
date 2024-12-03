using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class ProductoHelper : IProductoHelper
    {
        IServiceRepository _repository;

        public ProductoHelper(IServiceRepository serviceRepository)
        {
            this._repository = serviceRepository;
        }

        ProductoViewModel Convertir (Producto producto)
        {
            return new ProductoViewModel
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

        Producto Convertir (ProductoViewModel producto)
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

        public ProductoViewModel AddProduct(ProductoViewModel producto)
        {
            HttpResponseMessage responseMessage = _repository.PostResponse("api/producto", Convertir(producto));
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
            }


            return producto;

        }

        public void DeleteProduct(int id)
        {
            HttpResponseMessage responseMessage = _repository.DeleteResponse("api/producto/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }



        }

        public ProductoViewModel EditProduct(ProductoViewModel ProductoViewModel)
        {
            HttpResponseMessage responseMessage = _repository.PutResponse("api/producto/" + ProductoViewModel.IdProducto.ToString(), Convertir(ProductoViewModel));
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }


            return ProductoViewModel;
        }

        public List<ProductoViewModel> GetAll()
        {
            HttpResponseMessage responseMessage = _repository.GetResponse("api/producto");
            List<Producto> productos = new List<Producto>();


            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                productos = JsonConvert.DeserializeObject<List<Producto>>(content);


            }

            List<ProductoViewModel> list = new List<ProductoViewModel>();


            foreach (var item in productos)
            {
                list.Add(Convertir(item));
            }
            return list;
        }

        public ProductoViewModel GetById(int id)
        {
            Producto producto = new Producto();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/producto/" + id.ToString());


            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                producto = JsonConvert.DeserializeObject<Producto>(content);


            }

            return Convertir(producto);
        }
    }
}
