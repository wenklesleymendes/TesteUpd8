using Domino.Entities;
using InfraEstrutura.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Services
{
    public class EstadoService
    {
        private readonly ClienteContext _context;

        public EstadoService(ClienteContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> ObterTodosEstadosAsync()
        {
            return await _context.Estados.Include(e => e.Cidades).ToListAsync();
        }

        public async Task<List<Cidade>> ObterCidadesPorEstadoIdAsync(int estadoId)
        {
            return await _context.Cidades.Where(c => c.EstadoId == estadoId).ToListAsync();
        }
    }
}
