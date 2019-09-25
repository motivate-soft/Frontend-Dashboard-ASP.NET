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
    
    public class MiparticipacionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string[] lines;

        public MiparticipacionController(ApplicationDbContext context)
        {
            _context = context;
            lines = System.IO.File.ReadAllLines(@"config.txt");
        }

        [Route("mi-participacion")]
        public IActionResult Index()
        {
            var url_par = HttpContext.Request.Query["u"];
            string xa = HttpContext.Request.Query["xa"];
            if (xa == null || xa == "")
            {
                return Redirect("/es/inicio");
            }
            Participation participation = _context.Participation.Where(p => p.url_par.Equals(url_par)).FirstOrDefault();
            if (participation == null)
            {
                return Redirect("/es/inicio");
            }
            var id_par = participation.id_par;
            var id_est = participation.id_est;
            var solicitar_adjunto = participation.solicitar_adjunto;
            var adjunto_adjunto = participation.adjunto_adjunto;
            var motivo_par = participation.motivo_par;
            var comentarios_par = participation.comentarios_par;
            ViewBag.motivo_par = motivo_par;
            ViewBag.comentarios_par = comentarios_par;
            
            ViewBag.id_par = id_par;
            var DOMAIN01 = "DOMAIN01";
            DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
            Random random = new Random();
            ViewBag.resend_url = DOMAIN01 + "/mod-adjuntar?u=" + url_par + "&xa=" + random.Next();
            if (id_est == 2) // Validado
            {
                ViewBag.status = "Validado";
            }
            else if(id_est == 3) //RECHAZADO
            {
                ViewBag.status = "Rechazado";
            } else
            {
                if(solicitar_adjunto == 1 && adjunto_adjunto == 1) //Actualizando
                {
                    ViewBag.status = "Actualizando";
                }
                else if(solicitar_adjunto == 1 && adjunto_adjunto == 0) // Modificando
                {
                    ViewBag.status = "Modificando";
                }
                else{ //Pendiente
                    ViewBag.status = "Pendiente";
                }
            }

            return View();
        }
        
    }
}