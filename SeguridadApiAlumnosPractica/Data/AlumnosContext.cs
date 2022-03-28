using Microsoft.EntityFrameworkCore;
using SeguridadApiAlumnosPractica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadApiAlumnosPractica.Data
{
    public class AlumnosContext : DbContext
    {
        public AlumnosContext
      (DbContextOptions<AlumnosContext> options) : base(options) { }
        public DbSet<Alumno> Alumnos { get; set; }

    }
}
