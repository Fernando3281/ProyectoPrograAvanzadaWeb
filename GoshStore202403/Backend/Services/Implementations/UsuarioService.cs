using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace Backend.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        IUnidadDeTrabajo Unidad;

        public UsuarioService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.Unidad = unidadDeTrabajo;
        }


        public bool Agregar(Usuario usuario)
        {
            Unidad.UsuarioDAL.Add(usuario);
            return Unidad.Complete();
        }

        public bool Editar(Usuario usuario)
        {
            Unidad.UsuarioDAL.Update(usuario);
            return Unidad.Complete();
        }

        public bool Eliminar(Usuario usuario)
        {
            Unidad.UsuarioDAL.Remove(usuario);
            return Unidad.Complete();
        }

        public Usuario Obtener(int id)
        {
            return Unidad.UsuarioDAL.Get(id);
        }

        public List<Usuario> Obtener()
        {
            return Unidad.UsuarioDAL.GetAll().ToList();
        }
    }
}
