using Entities.Entities;

namespace Backend.Services.Interfaces
{
    public interface IUsuarioService
    {

        bool Agregar(Usuario entity);
        bool Editar(Usuario entity);
        bool Eliminar(Usuario entity);
        Usuario Obtener(int id);
        List<Usuario> Obtener();
    }
}
