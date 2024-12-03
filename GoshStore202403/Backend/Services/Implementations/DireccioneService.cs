using Backend.DTO;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class DireccioneService : IDireccioneService
    {
        IUnidadDeTrabajo _unidad;

        public DireccioneService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this._unidad = unidadDeTrabajo;
        }

        Direccione Convertir(DireccioneDTO direccione)
        {
            return new Direccione
            {
                IdDireccion= direccione.IdDireccion,
                DireccionEnvio = direccione.DireccionEnvio,
                Ciudad = direccione.Ciudad,
                CodigoPostal = direccione.CodigoPostal
            };
        }

        DireccioneDTO Convertir (Direccione direccione)
        {
            return new DireccioneDTO
            {
                IdDireccion = direccione.IdDireccion,
                DireccionEnvio = direccione.DireccionEnvio,
                Ciudad = direccione.Ciudad,
                CodigoPostal = direccione.CodigoPostal
            };
        }


        public DireccioneDTO GetByID(int id)
        {
            try
            {
                return Convertir(_unidad.DireccioneDAL.Get(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
            
        }

        public List<DireccioneDTO> GetDireccione()
        {
            var direcciones = _unidad.DireccioneDAL.GetAll();
            List<DireccioneDTO> direccioneList = new List<DireccioneDTO>();
            foreach (var item in direcciones)
            {
                direccioneList.Add(Convertir(item));
            }
            return direccioneList;
        }

        public bool Add(DireccioneDTO direccione)
        {
            try
            {
                Direccione entity = Convertir(direccione);
                _unidad.DireccioneDAL.Add(entity);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al agregar una dirección a la base de datos.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado", ex);
            }
        }

        public bool Update(DireccioneDTO direccione)
        {
            try
            {
                var entityExistence = _unidad.DireccioneDAL.Get(direccione.IdDireccion);
                if (entityExistence == null)
                {
                    throw new Exception("Direccón no encontrada.");
                }
                entityExistence.DireccionEnvio = direccione.DireccionEnvio;
                entityExistence.Ciudad = direccione.Ciudad;
                entityExistence.CodigoPostal = direccione.CodigoPostal;

                _unidad.DireccioneDAL.Update(entityExistence);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al editar dirección.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public bool Delete(DireccioneDTO direccione)
        {
            try
            {
                Direccione entity = Convertir(direccione);
                _unidad.DireccioneDAL.Remove(entity);
                return _unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar usuario de la base de datos", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado", ex);
            }
        }
    }
}
