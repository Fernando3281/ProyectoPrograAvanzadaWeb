using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IUsuarioHelper
    {
        List<UsuarioViewModel> GetUsuarios();
        UsuarioViewModel GetUsuario(int id);
        UsuarioViewModel Add(UsuarioViewModel usuario);
        UsuarioViewModel Update(int id, UsuarioViewModel usuario);
        void Delete(int id);


    }
}
