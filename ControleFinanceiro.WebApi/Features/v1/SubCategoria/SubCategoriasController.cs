using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFinanceiro.Domain.Exceptions;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.WebApi.Dtos.SubCategoria;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.WebApi.Features.v1.SubCategoria
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubCategoriasController : ControllerBase
    {
        private readonly ISubCategoriaService subCategoriaService;

        public SubCategoriasController(ISubCategoriaService subCategoriaService)
        {
            this.subCategoriaService = subCategoriaService ?? throw new ArgumentNullException(nameof(subCategoriaService));
        }

        // GET: api/values
        [HttpGet]
        //[ProducesResponseType(typeof(SubCategoriaCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<RetornaSubCategoriaGet>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await subCategoriaService.RetornaSubCategoriasAsync();
            var categorias = Mapper.Map<IEnumerable<RetornaSubCategoriaGet>>(result);

            return Ok(categorias);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await subCategoriaService.RetornaSubCategoriaAsync(id);

            var categoria = Mapper.Map<RetornaSubCategoriaGet>(result);

            return Ok(categoria);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncluiSubCategoriaPost incluiCategoriaPost)
        {
            var categoria = Mapper.Map<SubCategoriaEntity>(incluiCategoriaPost);

            await subCategoriaService.IncluiSubCategoriaAsync(categoria);

            return Ok(categoria);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizaSubCategoriaPut atualizaCategoriaPut)
        {
            var categoria = Mapper.Map<SubCategoriaEntity>(atualizaCategoriaPut);
            categoria.id = id;

            try
            {
                await subCategoriaService.AtualizaSubCategoriaAsync(categoria);

                return Ok(categoria);
            }
            catch (SubCategoriaCoreException ex) when (ex.Errors.Any(c => c.Key == SubCategoriaCoreError.SubCategoriaNaoEncontrado.Key))
            {
                return NotFound(SubCategoriaCoreError.SubCategoriaNaoEncontrado);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await subCategoriaService.ExcluiSubCategoriaAsync(id);

                return Ok();
            }
            catch (SubCategoriaCoreException ex) when (ex.Errors.Any(c => c.Key == SubCategoriaCoreError.SubCategoriaNaoEncontrado.Key))
            {
                return NotFound(SubCategoriaCoreError.SubCategoriaNaoEncontrado);
            }
        }
    }
}

