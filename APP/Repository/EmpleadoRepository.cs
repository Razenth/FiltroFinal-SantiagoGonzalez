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
    public class EmpleadoRepository : GenericRepositoryInt<Empleado>, IEmpleado
    {
        private readonly GardenContext _context;
        public EmpleadoRepository(GardenContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> Top20MostSellerProducts()
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

        public async Task<IEnumerable<Object>> NotSellsRepresentEmployee()
        {
            var results = await _context.Empleados
            .Where(e => e.Puesto != "Representante Ventas")
            .Select(e => new { nombre = $"{e.Nombre} {e.Apellido1} {e.Apellido2}", e.Puesto })
            .ToListAsync();
            return results;
        }
    }
}