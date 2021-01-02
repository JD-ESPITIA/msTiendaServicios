using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.libro.Aplicacion;

namespace TiendaServicios.api.libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {

        private readonly IMediator _mediator;
        public LibroMaterialController(IMediator mediator)
        {
            _mediator = mediator; // se inicializa en startup.cs
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            //http://localhost:63236/api/libromaterial
            /*
             * {
                "titulo": "La importancia de morir a tiempo",
                "fechaPublicacion": "1998-10-11",
                "autorLibro": "bc2de97a-4d08-4b83-a8b8-08d9dd8ee023"
                }
             */
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDTO>>> GetLibros()
        {
            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDTO>> GetLibroUnico(Guid Id)
        {
            //http://localhost:63236/api/libromaterial/5b76485f-0c45-4abc-0e85-08d8af4a13b9
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = Id });
        }
    }
}
