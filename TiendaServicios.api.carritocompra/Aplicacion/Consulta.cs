using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.carritocompra.Persistencia;
using TiendaServicios.api.carritocompra.RemoteInterface;

namespace TiendaServicios.api.carritocompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDto> {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibrosService _libroService;

            public Manejador(CarritoContexto contexto, ILibrosService libroservice)
            {
                _contexto = contexto;
                _libroService = libroservice;
            }
            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Se implementa la interfaz
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(obj => obj.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(obj => obj.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var listaCarritoDTO = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objLibro.Titulo,
                            FechaPublicacion = objLibro.FechaPublicacion,
                            LibroId = objLibro.LibreriaMaterialId
                        };

                        listaCarritoDTO.Add(carritoDetalle);

                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDTO
                };

                return carritoSesionDto;

            }
        }
    }
}
