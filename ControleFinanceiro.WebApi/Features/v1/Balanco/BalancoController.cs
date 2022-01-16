using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.WebApi.Dtos.Balanco;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControleFinanceiro.WebApi.Features.v1.Balanco
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BalancoController : ControllerBase
    {
        private readonly IBalancoService balancoService;

        public BalancoController(IBalancoService balancoService)
        {
            this.balancoService = balancoService ?? throw new ArgumentNullException(nameof(balancoService));
        }

        // GET: api/values
        [HttpGet]
        //[ProducesResponseType(typeof(CategoriaCoreException), 400)]
        [ProducesResponseType(typeof(IEnumerable<BalancoOutput>), 200)]
        public async Task<IActionResult> Get([FromQuery] BalancoInput filtro)
        {
            var mapFiltro = Mapper.Map<BalancoFiltro>(filtro);
            var result = await balancoService.RetornaBalanco(mapFiltro);
            var balanco = Mapper.Map<BalancoOutput>(result);

            return Ok(balanco);
        }
    }
}

