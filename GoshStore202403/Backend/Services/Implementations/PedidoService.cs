using Backend.DTO;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class PedidoService : IPedidoService
    {
        IUnidadDeTrabajo Unidad;

        public PedidoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.Unidad = unidadDeTrabajo;
        }

        #region MetodosConvertir
        Pedido Convertir(PedidoDTO pedido)
        {
            return new Pedido
            {
                IdPedido = pedido.IdPedido,
                IdUsuario = pedido.IdUsuario,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado
            };
        }

        PedidoDTO Convertir(Pedido pedido)
        {
            return new PedidoDTO
            {
                IdPedido = pedido.IdPedido,
                IdUsuario = pedido.IdUsuario,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado
            };
        }

        #endregion

        #region CRUD

        public bool Agregar(PedidoDTO pedido)
        {
            try
            {
                Pedido entity = Convertir(pedido);
                Unidad.PedidoDAL.Add(entity);
                return Unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar el pedido a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public bool Editar(PedidoDTO pedido)
        {
            try
            {
                var entityExistente = Unidad.PedidoDAL.Get(pedido.IdPedido);
                if (entityExistente == null)
                {
                    throw new Exception("Pedido no encontrado.");
                }
                entityExistente.IdUsuario = pedido.IdUsuario;
                entityExistente.FechaPedido = pedido.FechaPedido;
                entityExistente.Estado = pedido.Estado;

                Unidad.PedidoDAL.Update(entityExistente);
                return Unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al editar el pedido.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public bool Eliminar(PedidoDTO pedido)
        {
            try
            {
                Pedido entity = Convertir(pedido);
                Unidad.PedidoDAL.Remove(entity);
                return Unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el pedido de la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public PedidoDTO Obtener(int id)
        {
            try
            {
                return Convertir(Unidad.PedidoDAL.Get(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public List<PedidoDTO> Obtener()
        {
            try
            {
                List<PedidoDTO> list = new List<PedidoDTO>();
                var pedidos = Unidad.PedidoDAL.GetAll().ToList();

                foreach (var item in pedidos)
                {
                    list.Add(Convertir(item));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        #endregion
    }
}
