using Backend.DTO;
using Backend.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace Backend.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        IUnidadDeTrabajo _unidad;

        public CategoriaService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this._unidad = unidadDeTrabajo;
        }

        CategoriaDTO Convertir(Categoria categoria)
        {
            return new CategoriaDTO
            {
                IdCategoria = categoria.IdCategoria,
                NombreCategoria = categoria.NombreCategoria
            };
        }

        Categoria Convertir (CategoriaDTO categoriaDTO)
        {
            return new Categoria
            {
                IdCategoria = categoriaDTO.IdCategoria,
                NombreCategoria = categoriaDTO.NombreCategoria
            };
        }


        public CategoriaDTO GetByID(int id)
        {
            var categoria = _unidad.CategoriaDAL.Get(id);
            return Convertir(categoria);
        }

        public List<CategoriaDTO> GetCategorias()
        {
            var categorias = _unidad.CategoriaDAL.GetAll();
            List<CategoriaDTO> categoriaList = new List<CategoriaDTO>();
            foreach (var categoria in categorias)
            {
                categoriaList.Add(Convertir(categoria));
            }
            return categoriaList;
        }
    }
}
