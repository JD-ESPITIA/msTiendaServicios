using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.autor.Modelo;
using TiendaServicios.api.autor.Persistencia;

namespace TiendaServicios.api.autor.Aplicacion
{
    // cqrs
    // para nuevo autor
    public class Nuevo
    {
        // se encarga de recibir los parámetros quecontroller
        public class Ejecuta: IRequest { 
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta> { 
            public EjecutaValidacion()
            {
                // Fluentvalidator from Nuget
                // Valida que el valor no sea vacio
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        // se implementa la lógica de la inserción en la bd
        public class Manejador : IRequestHandler<Ejecuta>
        {

            public readonly ContextoAutor _contexto;
            // instancio inyecto contexto Autor
            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto; // inject
            }
            // se implementa la interface IRequestHandler

            /// <summary>
            /// 
            /// </summary>
            /// <param name="request">Obj a insertar</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // 0 significa error


                // Crea instancia autor
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    Apellido = request.Apellido,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };

                _contexto.AutorLibro.Add(autorLibro); //se agrega a un contexto
                // ahora a db
                var valor = await _contexto.SaveChangesAsync(); // Inserta la acción en db y retora un # de transacciones realizadas al ejecutar la transacción

                if (valor > 0)
                {
                    return Unit.Value;
                } else
                {
                    throw new Exception("No se pudo insertar el autor del libro");
                }

            }
        }



    }
}
