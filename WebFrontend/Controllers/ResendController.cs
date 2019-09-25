using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFrontend.Data;
using WebFrontend.Models;

namespace WebFrontend.Controllers
{
    public class ResendController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResendController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("mod-adjuntar")]
        public IActionResult Index()
        {
            var url_par = HttpContext.Request.Query["u"];
            string xa = HttpContext.Request.Query["xa"];
            if(xa == null || xa == "")
            {
                return Redirect("/es/inicio");
            }
            Participation participation = _context.Participation.Where(p=>p.url_par.Equals(url_par)).FirstOrDefault();
            if(participation == null)
            {
                return Redirect("/es/inicio");
            }
            var id_est = participation.id_est;
            var solicitar_adjunto = participation.solicitar_adjunto;
            var adjunto_adjunto = participation.adjunto_adjunto;

            //modify
            if(id_est == 1 && solicitar_adjunto == 1 && adjunto_adjunto == 0)
            {

            }
            else
            {
                return Redirect("/es/inicio");
            }

            var id_tall = participation.id_tall;
            var taller = _context.Talleres.Where(t => t.id_tall.Equals(id_tall)).FirstOrDefault();
            ViewBag.url_par = url_par;
            ViewBag.taller = taller.razonsocial_tall + "(" + taller.direccion_tall + ", " + taller.poblacion_tall + ", " + taller.poblacion_tall + ", " + taller.cp_tall + ", " + taller.provincia_tall + ")";
            ViewBag.nombre_par = participation.nombre_par;
            ViewBag.apellidos_par = participation.apellidos_par;
            ViewBag.email_par = participation.email_par;
            ViewBag.nacionalidad_par = participation.nacionalidad_par;
            ViewBag.telefono_par = participation.telefono_par;
            return View();
        }

        [Route("mod-adjuntar-ok")]
        public IActionResult Result(string test)
        {
            return View();
        }
    }
}