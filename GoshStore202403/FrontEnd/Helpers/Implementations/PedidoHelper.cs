using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class PedidoHelper : IPedidoHelper
    {
        IServiceRepository _ServiceRepository;

        public PedidoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        Pedido Convertir(PedidoViewModel pedido)
        {
            return new Pedido
            {
                IdPedido = pedido.IdPedido,
                IdUsuario = pedido.IdUsuario,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
            };
        }

        public PedidoViewModel Add(PedidoViewModel pedido)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Pedido", Convertir(pedido));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return pedido;
        }

        public PedidoViewModel GetPedido(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pedido/" + id.ToString());
            Pedido pedido = new Pedido();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pedido = JsonConvert.DeserializeObject<Pedido>(content);
            }

            PedidoViewModel resultado = new PedidoViewModel
            {
                IdPedido = pedido.IdPedido,
                IdUsuario = pedido.IdUsuario,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado
            };
            return resultado;
        }

        public List<PedidoViewModel> GetPedidos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pedido");
            List<Pedido> pedidos = new List<Pedido>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pedidos = JsonConvert.DeserializeObject<List<Pedido>>(content);
            }

            List<PedidoViewModel> resultado = new List<PedidoViewModel>();
            foreach (var item in pedidos)
            {
                resultado.Add(
                    new PedidoViewModel
                    {
                        IdPedido = item.IdPedido,
                        IdUsuario = item.IdUsuario,
                        FechaPedido = item.FechaPedido,
                        Estado = item.Estado,
                    }
                );
            }
            return resultado;
        }

        public PedidoViewModel Update(int id, PedidoViewModel pedido)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse($"api/Pedido/{id}", Convertir(pedido));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return pedido;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Pedido/" + id.ToString());
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error al eliminar pedido: " + responseMessage.ReasonPhrase);
            }
        }
    }
}
