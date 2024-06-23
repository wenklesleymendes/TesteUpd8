using Microsoft.AspNetCore.Mvc;
using Domino.Entities;
using System.Threading.Tasks;
using Aplicacao.Services;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        public IActionResult Cadastro()
        {
            var model = new Cliente();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _clienteService.AdicionarClienteAsync(cliente);
                    _logger.LogInformation("Cliente salvo com sucesso.");
                    TempData["SuccessMessage"] = "Cliente salvo com sucesso!";
                    return RedirectToAction("Consulta");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao salvar o cliente.");
                    ModelState.AddModelError("", "Erro ao salvar o cliente. Tente novamente.");
                }
            }
            return View(cliente);
        }

        public async Task<IActionResult> Consulta()
        {
            var clientes = await _clienteService.ObterTodosClientesAsync();
            return View(clientes);
        }
    }
}
