using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.libro.Modelo;
using TiendaServicios.api.libro.Persistencia;

namespace TiendaServicios.api.libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidacion: AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                // para que no sean valores null ni vacíos
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        // Lógica de la transacción
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contexto;

            public Manejador(ContextoLibreria contexto)
            {
                // inyección del contexto
                _contexto = contexto;
            }

            // se implementa la interfaz
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // inserción a db con entityFramework core

                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro
                };


                // para insertar, se añade el obj al contexto
                _contexto.LibreriaMaterial.Add(libro);
                // se confirma que se agerga un nuevo libro a la entidad
                var cantidadRegistros = await _contexto.SaveChangesAsync(); // se dispara el evento

                if (cantidadRegistros > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo añadir un libro");
            }
        }
    }
}
