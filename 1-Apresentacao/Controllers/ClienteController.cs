using Microsoft.AspNetCore.Mvc;
using Domino.Entities;
using System.Threading.Tasks;
using Aplicacao.Services;

namespace Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
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
                await _clienteService.AdicionarClienteAsync(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObterTodosClientesAsync();
            return View(clientes);
        }
    }
}
