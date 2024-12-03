using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace FrontEnd.Helpers.Implementations
{
    public class CategoriaHelper : ICategoriaHelper
    {
        IServiceRepository _repository;

        public CategoriaHelper(IServiceRepository serviceRepository)
        {
            this._repository = serviceRepository;
        }

        CategoriaViewModel Convertir (Categoria categoria)
        {
            return new CategoriaViewModel
            {
                IdCategoria = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria
            };
        }

        Categoria Convertir (CategoriaViewModel categoria)
        {
            return new Categoria
            {
                IdCategoria = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria
            };
        }

        public List<CategoriaViewModel> GetAll()
        {
            HttpResponseMessage responseMessage = _repository.GetResponse("api/categoria");
            List<Categoria> categorias = new List<Categoria>();


            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                categorias = JsonConvert.DeserializeObject<List<Categoria>>(content);


            }

            List<CategoriaViewModel> list = new List<CategoriaViewModel>();


            foreach (var item in categorias)
            {
                list.Add(Convertir(item));
            }
            return list;
        }

        public CategoriaViewModel GetById(int id)
        {
            Categoria categoria = new Categoria();
            HttpResponseMessage responseMessage = _repository.GetResponse("api/categoria/" + id.ToString());


            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                categoria = JsonConvert.DeserializeObject<Categoria>(content);


            }

            return Convertir(categoria);
        }
    }
}
