using Microsoft.AspNetCore.Mvc;
using SeguridadApiAlumnosPractica.Data;
using SeguridadApiAlumnosPractica.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeguridadApiAlumnosPractica.Repositories
{
    public class RepositoryAlumnos
    {
        private AlumnosContext context;

        public RepositoryAlumnos(AlumnosContext context)
        {
            this.context = context;
        }

        public List<Alumno> GetAlumnos()
        {
            return this.context.Alumnos.ToList();
        }

        public Alumno FindAlumno(int idalumno)
        {
            return this.context.Alumnos.SingleOrDefault(x => x.IdAlumno == idalumno);
        }

        public Alumno ExisteAlumno(string nombre, string apellido)
        {
            var consulta = from datos in this.context.Alumnos
                           where datos.Nombre == nombre
                           && datos.Apellidos == apellido
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return consulta.First();
            }
        }

        public ActionResult<List<Alumno>> GetAlumnosCurso(string curso)
        {
            return this.context.Alumnos.Where(x => x.Curso == curso).ToList(); 
        }

        public void InsertAlumno(string curso, string nombre, string apellidos, int nota)
        {
            int id = this.GetMaxId();
            Alumno alumno = new Alumno()
            {
                IdAlumno = id,
                Curso = curso,
                Nombre = nombre,
                Apellidos = apellidos,
                Nota = nota
            };
            this.context.Add(alumno);
            this.context.SaveChanges();
        }

        public void DeleteAlumno(int id)
        {
            Alumno alumno = this.FindAlumno(id);
            this.context.Remove(alumno);
            this.context.SaveChanges();
        }

        public void UpdateAlumno(int id, string curso, string nombre, string apellidos, int nota)
        {
            Alumno alumno = this.FindAlumno(id);
            alumno.Curso = curso;
            alumno.Nombre = nombre;
            alumno.Apellidos = apellidos;
            alumno.Nota = nota;
            this.context.SaveChanges();
        }

        public int GetMaxId()
        {
            int id = 1;
            if (this.context.Alumnos.Count() > 0)
            {
                int max = this.context.Alumnos.Max(x => x.IdAlumno);
                id = max + 1;
                return id;
            }
            else
            {
                return 1;
            }
        }
    }
}
