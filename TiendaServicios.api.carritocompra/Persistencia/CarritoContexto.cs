using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.carritocompra.Modelo;

namespace TiendaServicios.api.carritocompra.Persistencia
{
    public class CarritoContexto: DbContext
    {
        public CarritoContexto(DbContextOptions<CarritoContexto> options): base(options)
        {}

        public DbSet<CarritoSesion> CarritoSesion { get; set; }

        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }

    //dotnet ef migrations add MigrationMySqlInicial --project TiendaServicios.api.carritocompra
    //  dotnet ef database update --project TiendaServicios.api.carritocompra
}
