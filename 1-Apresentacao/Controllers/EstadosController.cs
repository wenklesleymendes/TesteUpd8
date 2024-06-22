using Microsoft.AspNetCore.Mvc;
using Aplicacao.Services;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosController : ControllerBase
    {
        private readonly EstadoService _estadoService;

        public EstadosController(EstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstados()
        {
            var estados = await _estadoService.ObterTodosEstadosAsync();
            return Ok(estados);
        }

        [HttpGet("{estadoId}/cidades")]
        public async Task<IActionResult> GetCidades(int estadoId)
        {
            var cidades = await _estadoService.ObterCidadesPorEstadoIdAsync(estadoId);
            return Ok(cidades);
        }
    }
}
