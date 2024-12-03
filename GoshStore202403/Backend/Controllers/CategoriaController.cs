using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            this._categoriaService = categoriaService;
        }


        // GET: api/<CategoriaController>
        [HttpGet]
        public ActionResult Get()
        {
            var result = _categoriaService.GetCategorias();
            return Ok(result);
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _categoriaService.GetByID(id);
            return Ok(result);
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
