using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.api.autor.Modelo
{
    /// <summary>
    /// Representa tabla autor en postgresql
    /// </summary>
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }


        // puede tener muchos grados académicos
        //colección de grados

        public ICollection<GradoAcademico> ListaGradoAcademico { get; set; }

        // La data que nosotros vayamos creando, va tener que fluir por diferentes ambientes
        // deben existir claves únicas que representen la data y que sirvan para hacer seguimiento
        public string AutorLibroGuid { get; set; } // permite darle seguimiento a un autorLibro, desde otra microservice

        //Guid -- Global Unique Identifier - Microsoft standard

        //-- Migration - code Firts ¡!¡!1¿
        // Instalar herramienta de migración: dotnet tool install --global dotnet-ef --version 3.1.2
        // dotnet ef migrations add MigracionPostgresInicial --project TiendaServicios.Api.Autor // crea Migrations/
        // dotnet ef database update --project TiendaServicios.Api.Autor // ejecuta scripts de carpeta migration

        //-- En PostgreSQL
        // __EFMigrationsHistory representa todas las migraciones realizadas

        // Patron CQRS -- para separar consultas y responsabilidades
        // La data fluye:
        // PostgreSQL <---linq---> EntityFramework <---Dependency Injection---> Web API <---http json/xml---> React Hooks

        // Libreria MediatR para implementar CQRS //nugget

    }
}
