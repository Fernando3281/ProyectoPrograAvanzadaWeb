using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class DetallePedidoHelper : IDetallePedidoHelper
    {
        IServiceRepository _serviceRepository;

        public DetallePedidoHelper(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        DetallePedido Convertir(DetallePedidoViewModel detalle)
        {
            return new DetallePedido
            {
                IdDetallePedido = detalle.IdDetallePedido,
                IdPedido = detalle.IdPedido,
                IdProducto = detalle.IdProducto,
                PrecioUnitario = detalle.PrecioUnitario,
                Cantidad = detalle.Cantidad,
            };
        }

        public DetallePedidoViewModel Add(DetallePedidoViewModel detalle)
        {
            HttpResponseMessage response = _serviceRepository.PostResponse("api/DetallePedido", Convertir(detalle));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return detalle;
        }

        public DetallePedidoViewModel GetDetallePedido(int id)
        {
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/DetallePedido/" + id.ToString());
            DetallePedido detalle = new DetallePedido();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                detalle = JsonConvert.DeserializeObject<DetallePedido>(content);
            }

            DetallePedidoViewModel resultado = new DetallePedidoViewModel
            {
                IdDetallePedido = detalle.IdDetallePedido,
                IdPedido = detalle.IdPedido ?? 0,
                IdProducto = detalle.IdProducto ?? 0,
                PrecioUnitario = detalle.PrecioUnitario ?? 0,
                Cantidad = detalle.Cantidad ?? 0
            };
            return resultado;
        }

        public List<DetallePedidoViewModel> GetDetallesPedido()
        {
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/DetallePedido");
            List<DetallePedido> detalles = new List<DetallePedido>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                detalles = JsonConvert.DeserializeObject<List<DetallePedido>>(content);
            }

            List<DetallePedidoViewModel> resultado = new List<DetallePedidoViewModel>();
            foreach (var item in detalles)
            {
                resultado.Add(
                    new DetallePedidoViewModel
                    {
                        IdDetallePedido = item.IdDetallePedido,
                        IdPedido = item.IdPedido ?? 0,
                        IdProducto = item.IdProducto ?? 0,
                        PrecioUnitario = item.PrecioUnitario ?? 0,
                        Cantidad = item.Cantidad ?? 0
                    }
                );
            }
            return resultado;
        }

        public DetallePedidoViewModel Update(int id, DetallePedidoViewModel detalle)
        {
            HttpResponseMessage response = _serviceRepository.PutResponse($"api/DetallePedido/{id}", Convertir(detalle));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return detalle;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _serviceRepository.DeleteResponse("api/DetallePedido/" + id.ToString());
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error al eliminar detalle del pedido: " + responseMessage.ReasonPhrase);
            }
        }
    }
}
