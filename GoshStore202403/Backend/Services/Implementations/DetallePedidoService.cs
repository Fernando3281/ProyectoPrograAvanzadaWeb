using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using FrontEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly IUnidadDeTrabajo _unidad;

        public DetallePedidoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidad = unidadDeTrabajo;
        }

        #region Métodos Convertir

        private DetallePedido Convertir(DetallePedidoDTO detallePedidoDTO)
        {
            return new DetallePedido
            {
                IdDetallePedido = detallePedidoDTO.IdDetallePedido,
                IdPedido = detallePedidoDTO.IdPedido,
                IdProducto = detallePedidoDTO.IdProducto,
                PrecioUnitario = detallePedidoDTO.PrecioUnitario,
                Cantidad = detallePedidoDTO.Cantidad
            };
        }

        private DetallePedidoDTO Convertir(DetallePedido detallePedido)
        {
            return new DetallePedidoDTO
            {
                IdDetallePedido = detallePedido.IdDetallePedido,
                IdPedido = detallePedido.IdPedido ?? 0,
                IdProducto = detallePedido.IdProducto ?? 0,
                NombreProducto = detallePedido.IdProductoNavigation?.NombreProducto ?? string.Empty,
                PrecioUnitario = detallePedido.PrecioUnitario ?? 0m,
                Cantidad = detallePedido.Cantidad ?? 0,
                Total = (detallePedido.PrecioUnitario ?? 0m) * (detallePedido.Cantidad ?? 0)
            };
        }

        #endregion

        #region CRUD

        public bool Agregar(DetallePedidoDTO detallePedidoDTO)
        {
            try
            {
                var entity = Convertir(detallePedidoDTO);
                _unidad.DetallePedidoDAL.Add(entity);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar el detalle de pedido a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public bool Editar(DetallePedidoDTO detallePedidoDTO)
        {
            try
            {
                var entityExistente = _unidad.DetallePedidoDAL.Get(detallePedidoDTO.IdDetallePedido);
                if (entityExistente == null)
                {
                    throw new Exception("Detalle de pedido no encontrado.");
                }

                entityExistente.IdPedido = detallePedidoDTO.IdPedido;
                entityExistente.IdProducto = detallePedidoDTO.IdProducto;
                entityExistente.PrecioUnitario = detallePedidoDTO.PrecioUnitario;
                entityExistente.Cantidad = detallePedidoDTO.Cantidad;

                _unidad.DetallePedidoDAL.Update(entityExistente);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al editar el detalle de pedido.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public bool Eliminar(DetallePedidoDTO detallePedidoDTO)
        {
            try
            {
                var entity = Convertir(detallePedidoDTO);
                _unidad.DetallePedidoDAL.Remove(entity);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el detalle de pedido en la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public DetallePedidoDTO Obtener(int id)
        {
            try
            {
                var detallePedido = _unidad.DetallePedidoDAL.Get(id);
                return detallePedido != null ? Convertir(detallePedido) : null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public List<DetallePedidoDTO> Obtener()
        {
            try
            {
                var list = new List<DetallePedidoDTO>();
                var detallesPedidos = _unidad.DetallePedidoDAL.GetAll().ToList();

                foreach (var item in detallesPedidos)
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