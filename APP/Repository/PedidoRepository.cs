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
    public class PedidoRepository : GenericRepositoryInt<Pedido>, IPedido
    {
        private readonly GardenContext _context;
        public PedidoRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> ProductsWithOutOrderWithDetails()
        {
            var results = await _context.Productos
                .Where(p => !p.DetallePedidos.Any())
                .Select(p => new
                {
                    p.Nombre,
                    p.Descripcion,
                    p.GamaNavigation.Imagen
                })
                .ToListAsync();
            return results;
        }
    }
}