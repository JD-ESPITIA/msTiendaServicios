using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.libro.Modelo;

namespace TiendaServicios.api.libro.Persistencia
{
    //dotnet ef migrations add MigracionPostgresInicial --project TiendaServicios.Api.Autor
    //    dotnet ef database update --project TiendaServicios.Api.Autor

    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options)
        {

        }

        public DbSet<LibreriaMaterial> LibreriaMaterial { get; set;}


    }
}
