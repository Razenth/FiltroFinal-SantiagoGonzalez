# FiltroFinal-SantiagoGonzalez
## 1. Devuelve el listado de clientes indicando el nombre del cliente y cuántos
pedidos ha realizado

## 2. Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos que no han sido entregados a
tiempo.
        [HttpGet("OffTimeDeliver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<string>> OffTimeDeliver()
        {
            var results = await _context.Pedidos
            .Where(p => p.FechaEntrega > p.FechaEsperada || p.FechaEntrega == null)
            .OrderBy(p => p.FechaEsperada)
            .Select(p => new { p.Id, p.CodigoCliente, p.FechaEsperada, p.FechaEntrega })
            .ToListAsync();

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

- Para realizar esta consulta lo que se realizó fue un metodo de extensión que me encuentre un pedido donde la Fecha de entrega fuese mayor a la fecha esperada, una vez me trajese esos resultados, realizo una nueva instancia donde almacene tanto el codigo del cliente, la fecha esperada y la fecha de entrega

## 3. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.

        [HttpGet("ProductsWithOutOrderWithDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<string>> ProductsWithOutOrderWithDetails()
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


            if (results == null)
            {
                return BadRequest();
            }
            return Ok(results);
        }

- Para esta consulta, realicé pasos parecidos al anterior, ya que en mi entidad Productos, verifiqué por medio de la tabla DetallePedidos, si existía alguna orden con ese producto. Si es así me retorna todos los productos y luego creo una nueva instancia que me dará la información que requiero.


// 4. Devuelve las oficinas donde no trabajan ninguno de los empleados que
// hayan sido los representantes de ventas de algún cliente que haya realizado
// la compra de algún producto de la gama Frutales.
        [HttpGet("GetOfficesWithEmployeeinGammaFrutales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<string>> GetOfficesWithEmployeeinGammaFrutales()
        {

            var results = await _context.Oficinas
    .Where(o => !o.Empleados.Any(e => e.Clientes.Any(c => c.Pedidos.Any(p => p.DetallePedidos.Any(dp => dp.CodigoProductoNavigation.GamaNavigation.Id == "Frutales")))))
    .ToListAsync();


            if (results == null)
            {
                return BadRequest();
            }
            return Ok(results);
        }

// 6. Devuelve un listado con el nombre, apellidos y puesto de aquellos empleados que no sean representantes de ventas.
        [HttpGet("NotSellsRepresentEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<string>> NotSellsRepresentEmployee()
        {
        var results = await _context.Empleados
        .Where(e => e.Puesto != "Representante Ventas")
        .Select(e => new { nombre = $"{e.Nombre} {e.Apellido1} {e.Apellido2}", e.Puesto })
        .ToListAsync();
        
            if (results == null)
            {
                return NotFound();
            }
        
            return Ok(results);
        }

// 8. Devuelve un listado de los 20 productos más vendidos y el número total de
unidades que se han vendido de cada uno. El listado deberá estar ordenado
por el número total de unidades vendidas.


        [HttpGet("GetTop20MostSellerProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<string>> GetTop20MostSellerProducts()
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
                    // if(results == false){
                    //     return BadRequest();
                    // }

            return Ok(results);
        }

// 10. Devuelve un listado de las diferentes gamas de producto que ha comprado cada cliente.
            [HttpGet("CustomersGammas")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<string>> CustomersGammas()
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
    
    
    
                if (results == null)
                {
                    return NotFound();
                }
    
                return Ok(results);
            }