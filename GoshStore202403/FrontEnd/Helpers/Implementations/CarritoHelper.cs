using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using FrontEnd.ApiModels;
using NuGet.Protocol.Core.Types;


namespace FrontEnd.Helpers.Implementations
{
    public class CarritoHelper : ICarritoHelper
    {
        IServiceRepository _repository;

        public CarritoHelper(IServiceRepository serviceRepository)
        {
            _repository = serviceRepository;
        }

        private async Task<CarritoViewModel> ConvertirAsync(Carrito apiModel)
        {
            // Realiza la llamada al backend para obtener los detalles del producto
            var response = _repository.GetResponse($"api/Producto/{apiModel.IdProducto}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var producto = Newtonsoft.Json.JsonConvert.DeserializeObject<Producto>(jsonData);


                return new CarritoViewModel
                {
                    IdCarrito = apiModel.IdCarrito,
                    IdProducto = apiModel.IdProducto,
                    NombreProducto = producto?.NombreProducto ?? "Desconocido",
                    Precio = producto?.Precio ?? 0,                          
                    Cantidad = apiModel.Cantidad
                };
            }

            // Si falla la llamada, retorna un ViewModel básico
            return new CarritoViewModel
            {
                IdCarrito = apiModel.IdCarrito,
                IdProducto = apiModel.IdProducto,
                NombreProducto = "Desconocido",
                Precio = 0,
                Cantidad = apiModel.Cantidad
            };
        }


        private Carrito Convertir(CarritoViewModel viewModel)
        {
            return new Carrito
            {
                IdCarrito = viewModel.IdCarrito,
                IdProducto = viewModel.IdProducto,
                Cantidad = viewModel.Cantidad,
                IdUsuario = ""
            };
        }

        public async Task<List<CarritoViewModel>> ObtenerCarritoAsync(string usuarioId)
        {
            var response = _repository.GetResponse("api/Carrito");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
   
                var carrito = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Carrito>>(jsonData);

                var carritoViewModels = new List<CarritoViewModel>();
                foreach (var item in carrito)
                {
                    var viewModel = await ConvertirAsync(item); 
                    carritoViewModels.Add(viewModel);
                }

                return carritoViewModels;
            }

            return new List<CarritoViewModel>();
        }

        public async Task<bool> AgregarAlCarritoAsync(CarritoViewModel item)
        {
            var apiModel = Convertir(item);
            Console.WriteLine($"Datos enviados al backend: {Newtonsoft.Json.JsonConvert.SerializeObject(apiModel)}");
            var response = _repository.PostResponse("api/carrito/Add", apiModel);
            Console.WriteLine($"Estado de la respuesta: {response.StatusCode}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarDelCarritoAsync(int idCarrito, int idProducto)
        {
            var response = _repository.DeleteResponse($"api/Carrito/Remove/{idProducto}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VaciarCarritoAsync(string usuarioId)
        {
            var response = _repository.DeleteResponse($"api/carrito/vaciar/{usuarioId}");
            return response.IsSuccessStatusCode;
        }
    }
}
