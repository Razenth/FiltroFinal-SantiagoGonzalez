using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace APP.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GardenContext _context;

        public UnitOfWork(GardenContext context)
        {
            _context = context;
        }

        private ClienteRepository _cliente;  //CiudadRepository _ciudad
        public ICliente Clientes
        {
            get
            {
                if (_cliente == null)
                {
                    _cliente = new ClienteRepository(_context);
                }
                return _cliente;
            }
        }

        private DetallePedidoRepository _detallePedido;  //CiudadRepository _ciudad
        public IDetallePedido DetallePedidos
        {
            get
            {
                if (_detallePedido == null)
                {
                    _detallePedido = new DetallePedidoRepository(_context);
                }
                return _detallePedido;
            }
        }

        private EmpleadoRepository _empleado;  //CiudadRepository _ciudad
        public IEmpleado Empleados
        {
            get
            {
                if (_empleado == null)
                {
                    _empleado = new EmpleadoRepository(_context);
                }
                return _empleado;
            }
        }

        private GamaProductoRepository _gamaProducto;  //CiudadRepository _ciudad
        public IGamaProducto GamaProductos
        {
            get
            {
                if (_gamaProducto == null)
                {
                    _gamaProducto = new GamaProductoRepository(_context);
                }
                return _gamaProducto;
            }
        }

        private OficinaRepository _oficina;  //CiudadRepository _ciudad
        public IOficina Oficinas
        {
            get
            {
                if (_oficina == null)
                {
                    _oficina = new OficinaRepository(_context);
                }
                return _oficina;
            }
        }

        private PagoRepository _pago;  //CiudadRepository _ciudad
        public IPago Pagos
        {
            get
            {
                if (_pago == null)
                {
                    _pago = new PagoRepository(_context);
                }
                return _pago;
            }
        }

        private PedidoRepository _pedido;  //CiudadRepository _ciudad
        public IPedido Pedidos
        {
            get
            {
                if (_pedido == null)
                {
                    _pedido = new PedidoRepository(_context);
                }
                return _pedido;
            }
        }

        private ProductoRepository _;  //CiudadRepository _ciudad
        public IProducto Productos
        {
            get
            {
                if (_ == null)
                {
                    _ = new ProductoRepository(_context);
                }
                return _;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}