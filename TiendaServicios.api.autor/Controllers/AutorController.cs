using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.autor.Aplicacion;
using TiendaServicios.api.autor.Modelo;

namespace TiendaServicios.api.autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            // http://localhost:53921/api/autor
            /*
             * {
                "nombre":"Diego",
                "apellido":"Segura",
                "FechaNacimiento":"1997-08-22"
            }
             */
            return await _mediator.Send(data); // Envia la data a la clase dentro de aplicación
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> getAutores()
        {
            return await _mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> getAutorLibro(string id)
        {
            // http://localhost:53921/api/autor/bc2de97a-4d08-4b83-a8b8-08d9dd8ee023
            return await _mediator.Send(new ConsultaFiltro.AutorUnico { AutorGuid = id });
        }
    }
}
