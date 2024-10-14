using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class UsuarioHelper : IUsuarioHelper
    {
        IServiceRepository _ServiceRepository;

        public UsuarioHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        Usuario Convertir(UsuarioViewModel usuario)
        {
            return new Usuario
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
            };
        }

        public UsuarioViewModel Add(UsuarioViewModel usuario)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Usuario", Convertir(usuario));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return usuario;
        }

        public UsuarioViewModel GetUsuario(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Usuario/" + id.ToString());
            Usuario usuario = new Usuario();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;


                usuario = JsonConvert.DeserializeObject<Usuario>(content);
            }

            UsuarioViewModel resultado = new UsuarioViewModel
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            };
            return resultado;
        }

        public List<UsuarioViewModel> GetUsuarios()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Usuario");
            List<Usuario> usuarios = new List<Usuario>();
            if (responseMessage != null)
            { 
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);
            }

            List<UsuarioViewModel> resultado = new List<UsuarioViewModel>();
            foreach (var item in usuarios)
            {
                resultado.Add(
                    new UsuarioViewModel
                    {
                        IdUsuario = item.IdUsuario,
                        Nombre = item.Nombre,
                        Correo = item.Correo,
                    }
                    );
            }
            return resultado;
        }

        public UsuarioViewModel Update(int id, UsuarioViewModel usuario)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse($"api/Usuario/{id}", Convertir(usuario));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
            }
            return usuario;
        }


        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Usuario/" + id.ToString());
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error al eliminar usuario: " + responseMessage.ReasonPhrase);
            }
        }
    }
}
