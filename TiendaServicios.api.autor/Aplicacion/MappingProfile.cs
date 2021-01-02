using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.autor.Modelo;

namespace TiendaServicios.api.autor.Aplicacion
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDTO>();
        }
    }
}
