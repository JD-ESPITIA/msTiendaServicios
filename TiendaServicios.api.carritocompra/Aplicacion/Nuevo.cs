using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.carritocompra.Modelo;
using TiendaServicios.api.carritocompra.Persistencia;

namespace TiendaServicios.api.carritocompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }


        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _contexto;
        
            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }
            
            
            // Se implementa la interfaz
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.CarritoSesion.Add(carritoSesion);
                var value = await _contexto.SaveChangesAsync();

                if (value==0)
                {
                    throw new Exception("Errores en la insersión del carrito");
                }

                // Se recupera el id que se genera en la db
                // magia
                int id = carritoSesion.CarritoSesionId;

                // ProductoLista lista de productos que envía el cliente
                // por cada elem de la lista se crea un nuevo objeto de tipo CarritoSesionDetalle
                foreach (var item in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id, // con el id respectivo
                        ProductoSeleccionado = item
                    };

                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }

                value = await _contexto.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }
}
