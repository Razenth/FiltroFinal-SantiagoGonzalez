using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmpleado : IGenericRepositoryInt<Empleado>
    {
        Task<IEnumerable<Object>> Top20MostSellerProducts();
        Task<IEnumerable<Object>> NotSellsRepresentEmployee();
    }
    
}