﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.carritocompra.Aplicacion;

namespace TiendaServicios.api.carritocompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator _imediator;
        public CarritoComprasController(IMediator mediator)
        {
            _imediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _imediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await _imediator.Send(new Consulta.Ejecuta {
            CarritoSesionId = id});
        }
    }
}
