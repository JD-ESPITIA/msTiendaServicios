using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.libro.Modelo;
using TiendaServicios.api.libro.Persistencia;

namespace TiendaServicios.api.libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico: IRequest<LibroMaterialDTO>
        {
            public Guid? LibroId { get; set; }

        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }


            //SE IMPLEMENTA LA INTERFAZ
            public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();

                if (libro == null)
                {
                    throw new Exception("No se encontró el libro");
                }

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDTO>(libro);

                return libroDto;

            }
        }
    }
}
