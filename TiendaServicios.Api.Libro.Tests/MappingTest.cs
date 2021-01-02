using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaServicios.api.libro.Aplicacion;
using TiendaServicios.api.libro.Modelo;

namespace TiendaServicios.Api.Libro.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest() {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>();
        }

    }
}
