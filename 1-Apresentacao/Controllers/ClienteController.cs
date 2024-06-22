using Aplicacao.Services;
using Domino.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        // Ação para exibir a página de cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        // Ação para processar o cadastro do cliente
        [HttpPost]
        public async Task<IActionResult> Cadastro(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _service.AdicionarClienteAsync(cliente);
                return RedirectToAction("Consulta");
            }
            return View(cliente);
        }

        // Ação para exibir a página de consulta
        public async Task<IActionResult> Consulta()
        {
            var clientes = await _service.ObterTodosClientesAsync();
            return View(clientes);
        }

        // Ação para excluir um cliente
        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            await _service.ExcluirClienteAsync(id);
            return RedirectToAction("Consulta");
        }
    }
}
