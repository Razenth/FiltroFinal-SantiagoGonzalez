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
    public class ClienteRepository : GenericRepositoryInt<Cliente>, ICliente
    {
        private readonly GardenContext _context;
        public ClienteRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Object>> OffTimeDeliver()
        {
            var results = await _context.Pedidos
            .Where(p => p.FechaEntrega > p.FechaEsperada || p.FechaEntrega == null)
            .OrderBy(p => p.FechaEsperada)
            .Select(p => new { p.Id, p.CodigoCliente, p.FechaEsperada, p.FechaEntrega })
            .ToListAsync();

            return results;
        }

        public async Task<IEnumerable<Object>> CustomersGammas()
        {
            var results = await _context.Clientes
                .Join(_context.Pedidos,
                    c => c.Id,
                    p => p.CodigoCliente,
                    (c, p) => new { Cliente = c, Pedido = p })
                .Join(_context.DetallePedidos,
                    cp => cp.Pedido.Id,
                    dp => dp.Id,
                    (cp, dp) => new { cp.Cliente, cp.Pedido, DetallePedido = dp })
                .Join(_context.Productos,
                    cpd => cpd.DetallePedido.CodigoProducto,
                    pr => pr.Id,
                    (cpd, pr) => new { cpd.Cliente, cpd.Pedido, cpd.DetallePedido, Producto = pr })
                .GroupBy(cpdp => new { cpdp.Cliente.Id, cpdp.Cliente.NombreCliente })
                .Select(group => new
                {
                    ClienteNombre = group.Key.NombreCliente,
                    GamasCompradas = string.Join(", ", group.Select(cpdp => cpdp.Producto.Gama).Distinct())
                })
                .ToListAsync();
            return results;
        }
    }
}