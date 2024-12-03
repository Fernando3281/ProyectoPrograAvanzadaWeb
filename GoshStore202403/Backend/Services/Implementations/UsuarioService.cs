using Backend.DTO;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        IUnidadDeTrabajo Unidad;

        public UsuarioService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.Unidad = unidadDeTrabajo;
        }

        #region MetodosConvertir
        Usuario Convertir(UsuarioDTO usuario)
        {
            return new Usuario
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            };
        }

        UsuarioDTO Convertir(Usuario usuario)
        {
            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            };
        }

        #endregion

        #region CRUD

        public bool Agregar(UsuarioDTO usuario)
        {
            try
            {
                Usuario entity = Convertir(usuario);
                Unidad.UsuarioDAL.Add(entity);
                return Unidad.Complete();
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

        public bool Editar(UsuarioDTO usuario)
        {
            try
            {
                var entityExistente = Unidad.UsuarioDAL.Get(usuario.IdUsuario);
                if (entityExistente == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }
                entityExistente.Nombre = usuario.Nombre;
                entityExistente.Correo = usuario.Correo;

                Unidad.UsuarioDAL.Update(entityExistente);
                return Unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al editar el usuario.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public bool Eliminar(UsuarioDTO usuario)
        {
            try
            {
                Usuario entity = Convertir(usuario);
                Unidad.UsuarioDAL.Remove(entity);
                return Unidad.Complete();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar el usuario de la base de datos.", dbEx);
            }
            catch (Exception ex) 
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public UsuarioDTO Obtener(int id)
        {
            try
            {
                return Convertir(Unidad.UsuarioDAL.Get(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public List<UsuarioDTO> Obtener()
        {
            try
            {
                List<UsuarioDTO> list = new List<UsuarioDTO>();
                var usuarios = Unidad.UsuarioDAL.GetAll().ToList();

                foreach ( var item in usuarios )
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
