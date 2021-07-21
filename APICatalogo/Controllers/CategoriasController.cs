using ApiCatalogo.Services;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;

        public CategoriasController(AppDbContext contexto, IConfiguration config)
        {
            _context = contexto;
            _configuration = config;
        }

        [HttpGet("autor")]
        public string GetAutor()
        {
            var autor = _configuration["autor"];
            var conexao = _configuration["ConnectionStrings:DefaultConnection"];
                return $"Autor: {autor} Conexão: {conexao}";
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {

            return _context.Categorias.Include(x => x.Produtos).ToList();
        }

        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuservico, string nome)
        {
            return meuservico.Saudacao(nome);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            try
            {
                return _context.Categorias.AsNoTracking().ToList();

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias do Banco de dados");
            }
        }

        [HttpGet("primeiro")]
        public ActionResult<Categoria> GetTwo()
        {

            try
            {
                return _context.Categorias.FirstOrDefault();

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter as categorias do Banco de dados");
            }
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking()
                    .FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"A categoria com o id = {id} não foi encontrada");
                }
                return categoria;
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter a categorias do Banco de dados");
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoria);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
              "Erro ao tentar criar uma nova categoria");
            }

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest($"Não foi possível atualizar a categoria com o id = {id}");
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok($"Categoria com id = {id} foi atualizada com sucesso");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar atualizar a categoria com id = {id}");
            }
          
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
                //var categoria = _context.Categorias.Find(id);

                if (categoria == null)
                {
                    return NotFound($"A categoria com id = {id} não foi encontrada");
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return categoria;

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao excluir a categoria com id = {id}");
            }
         
        }
    }
}

