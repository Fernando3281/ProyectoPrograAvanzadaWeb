using Backend.DTO;
using Entities.Entities;

namespace Backend.Services.Interfaces
{
    public interface IUsuarioService
    {

        bool Agregar(UsuarioDTO entity);
        bool Editar(UsuarioDTO entity);
        bool Eliminar(UsuarioDTO entity);
        UsuarioDTO Obtener(int id);
        List<UsuarioDTO> Obtener();
    }
}
