using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.autor.Modelo;
using TiendaServicios.api.autor.Persistencia;

namespace TiendaServicios.api.autor.Aplicacion
{
    /// <summary>
    /// procesar y devolver autores de la dn
    /// </summary>
    public class Consulta
    {
        // IRequest Sirvepara recibir datos del cliente y para devolverle datos al cliente
        public class ListaAutor : IRequest<List<AutorDTO>> { }
        public class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapp)
            {
                // para db
                _contexto = contexto;

                // para dto
                _mapper = mapp;
            }
            // Se implementa la interfaz ctrl .
            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.AutorLibro.ToListAsync();

                // ahora pasara por un dto para modelar la data
                var autoresDTO = _mapper.Map<List<AutorLibro>, List<AutorDTO>>(autores);

                return autoresDTO;
            }
        }
    }
}
