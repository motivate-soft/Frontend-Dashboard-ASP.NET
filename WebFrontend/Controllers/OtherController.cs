using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebFrontend.Controllers
{
    public class OtherController : Controller
    {
        [Route("preguntas-frecuentes")]
        public IActionResult Preguntas()
        {
            return View();
        }

        [Route("bases-legales")]
        public IActionResult BasesLegales()
        {
            return View();
        }

        [Route("politica-privacidad")]
        public IActionResult Privacidad()
        {
            return View();
        }

        [Route("politica-cookies")]
        public IActionResult Cookies()
        {
            return View();
        }
    }
}
