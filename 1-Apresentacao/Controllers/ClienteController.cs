using Microsoft.AspNetCore.Mvc;
using Domino.Entities;
using System.Threading.Tasks;
using Aplicacao.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _clienteService.ObterClientePorIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Json(new
            {
                id = cliente.Id,
                nome = cliente.Nome,
                cpf = cliente.Cpf,
                dataNascimento = cliente.DataNascimento.ToString("yyyy-MM-dd"), // Formato compatível com input date
                estado = cliente.Estado,
                cidade = cliente.Cidade,
                sexo = cliente.Sexo
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            try
            {
                await _clienteService.AtualizarClienteAsync(cliente);
                _logger.LogInformation("Cliente atualizado com sucesso.");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o cliente.");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clienteService.ExcluirClienteAsync(id);
                _logger.LogInformation("Cliente removido com sucesso.");
                TempData["SuccessMessage"] = "Cliente removido com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o cliente.");
                TempData["ErrorMessage"] = "Erro ao remover o cliente. Tente novamente.";
            }
            return RedirectToAction("Consulta");
        }
    }
}
