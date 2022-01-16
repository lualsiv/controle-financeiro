using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFinanceiro.Domain.Exceptions;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.WebApi.Dtos.Categoria;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            this.categoriaService = categoriaService ?? throw new ArgumentNullException(nameof(categoriaService));
        }

        // GET: api/values
        [HttpGet]
        //[ProducesResponseType(typeof(CategoriaCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<RetornaCategoriaGet>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await categoriaService.RetornaCategoriasAsync();
            var categorias = Mapper.Map<IEnumerable<RetornaCategoriaGet>>(result);

            return Ok(categorias);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await categoriaService.RetornaCategoriaAsync(id);

            var categoria = Mapper.Map<RetornaCategoriaGet>(result);

            return Ok(categoria);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IncluiCategoriaPost incluiCategoriaPost)
        {
            var categoria = Mapper.Map<CategoriaEntity>(incluiCategoriaPost);

            await categoriaService.IncluiCategoriaAsync(categoria);

            return Ok(categoria);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizaCategoriaPut atualizaCategoriaPut)
        {
            var categoria = Mapper.Map<CategoriaEntity>(atualizaCategoriaPut);
            categoria.id = id;

            try
            {
                await categoriaService.AtualizaCategoriaAsync(categoria);

                return Ok(categoria);
            }
            catch (CategoriaCoreException ex) when (ex.Errors.Any(c => c.Key == CategoriaCoreError.CategoriaNaoEncontrado.Key))
            {
                return NotFound(CategoriaCoreError.CategoriaNaoEncontrado);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {            
                await categoriaService.ExcluiCategoriaAsync(id);

                return Ok();
            }
            catch (CategoriaCoreException ex) when (ex.Errors.Any(c => c.Key == CategoriaCoreError.CategoriaNaoEncontrado.Key))
            {
                return NotFound(CategoriaCoreError.CategoriaNaoEncontrado);
            }
        }
    }
}

