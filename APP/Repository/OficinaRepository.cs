using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace APP.Repository
{
    public class OficinaRepository : GenericRepositoryVarchar<Oficina>, IOficina
    {
        private readonly GardenContext _context;
        public OficinaRepository(GardenContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Object>> GetOfficesWithEmployeeinGammaFrutales()
        {

            var results = await _context.Oficinas
                .Where(o => !o.Empleados.Any(e => e.Clientes.Any(c => c.Pedidos.Any(p => p.DetallePedidos.Any(dp => dp.CodigoProductoNavigation.GamaNavigation.Id == "Frutales")))))
                .ToListAsync();
            return results;
        }
    }
}