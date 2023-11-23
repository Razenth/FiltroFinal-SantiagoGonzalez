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
    public class ProductoRepository : GenericRepositoryVarchar<Producto>, IProducto
    {
        private readonly GardenContext _context;
        public ProductoRepository(GardenContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetTop20MostSellProducts()
        {

        var results = await _context.Productos
            .OrderByDescending(p => p.DetallePedidos.Sum(dp => dp.Cantidad))
            .Take(20)
            .Select(p => new
            {
                NombreProducto = p.Nombre,
                UnidadesVendidas = p.DetallePedidos.Sum(dp => dp.Cantidad)
            })
            .ToListAsync();
            return results;
        }
    }
}