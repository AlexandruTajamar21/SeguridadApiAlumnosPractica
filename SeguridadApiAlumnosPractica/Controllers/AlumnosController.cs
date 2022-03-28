using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeguridadApiAlumnosPractica.Model;
using SeguridadApiAlumnosPractica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadApiAlumnosPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private RepositoryAlumnos repo;

        public AlumnosController(RepositoryAlumnos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<Alumno>> GetAlumnos()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;
            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);
            if (alumno.Nombre == "Javier" || alumno.Nombre == "Victor")
            {

                return this.repo.GetAlumnos();
            }
            else
            {

                return this.repo.GetAlumnosCurso(alumno.Curso);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Alumno> FindAlumno(int id)
        {
            return this.repo.FindAlumno(id);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Boolean> IsertAlumno(Alumno alum)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;
            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);
            if(alumno.Nombre == "Javier" || alumno.Nombre == "Miguel" || alumno.Curso == alum.Curso)
            {
                this.repo.InsertAlumno(alum.Curso, alum.Nombre, alum.Apellidos, alum.Nota);
                return true;
            }
            return false;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Boolean> DeleteAlumno(int id)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;
            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);
            if (alumno.Nombre == "Javier" || alumno.Nombre == "Miguel")
            {
                this.repo.DeleteAlumno(id);
                return true;
            }
            return false;
        }
        [HttpPut]
        public ActionResult<Alumno> UpdateAlumno(Alumno alum)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;
            Alumno alumno = JsonConvert.DeserializeObject<Alumno>(jsonAlumno);
            if (alumno.Nombre == "Javier" || alumno.Nombre == "Miguel" || alumno.IdAlumno == alum.IdAlumno)
            {
                this.repo.InsertAlumno(alum.Curso, alum.Nombre, alum.Apellidos, alum.Nota);
                return alum;
            }
            return Unauthorized();
        }
    }
}
