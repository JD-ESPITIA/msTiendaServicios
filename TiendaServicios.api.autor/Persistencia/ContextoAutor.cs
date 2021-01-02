using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.autor.Modelo;

namespace TiendaServicios.api.autor.Persistencia
{
    public class ContextoAutor: DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options) { } //TODO REVISAR

        //Las clases comunes se convierten a clases de tipo entidad
        // así se podrán mapear con las tablas en db
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}
