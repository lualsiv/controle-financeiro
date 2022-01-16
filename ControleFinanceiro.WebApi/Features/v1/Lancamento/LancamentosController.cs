using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFinanceiro.Domain.Exceptions;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.WebApi.Dtos.Lancamento;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.WebApi.Features.v1.Lancamento
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LancamentosController : ControllerBase
    {
        private readonly ILancamentoService lancamentoService;

        public LancamentosController(ILancamentoService lancamentoService)
        {
            this.lancamentoService = lancamentoService ?? throw new ArgumentNullException(nameof(lancamentoService));
        }

        [HttpGet]
        //[ProducesResponseType(typeof(CategoriaCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<LancamentoOutput>), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await lancamentoService.RetornaLancamentosAsync();
            var categorias = Mapper.Map<IEnumerable<LancamentoOutput>>(result);

            return Ok(categorias);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LancamentoOutput), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await lancamentoService.RetornaLancamentoAsync(id);

            var lancamento = Mapper.Map<LancamentoOutput>(result);

            return Ok(lancamento);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LancamentoInput lancamentoInput)
        {
            var lancamento = Mapper.Map<LancamentoEntity>(lancamentoInput); 

            await lancamentoService.IncluiLancamentoAsync(lancamento);

            return Ok(lancamentoInput);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LancamentoInput lancamentoInput)
        {
            var lancamento = Mapper.Map<LancamentoEntity>(lancamentoInput);
            lancamento.id = id;

            try
            {
                await lancamentoService.AtualizaLancamentoAsync(lancamento);

                return Ok(lancamentoInput);
            }
            catch (LancamentoCoreException ex) when (ex.Errors.Any(c => c.Key == LancamentoCoreError.LancamentoNaoEncontrado.Key))
            {
                return NotFound(LancamentoCoreError.LancamentoNaoEncontrado);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await lancamentoService.ExcluiLancamentoAsync(id);

                return Ok();
            }
            catch (LancamentoCoreException ex) when (ex.Errors.Any(c => c.Key == LancamentoCoreError.LancamentoNaoEncontrado.Key))
            {
                return NotFound(LancamentoCoreError.LancamentoNaoEncontrado);
            }
        }
    }
}

