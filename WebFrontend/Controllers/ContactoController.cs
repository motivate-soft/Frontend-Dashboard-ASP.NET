using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebFrontend.Controllers
{
    public class ContactoController : Controller
    {
        [Route("contacto")]
        public IActionResult Contacto()
        {
            return View();
        }

        [Route("contacto-enviado")]
        public IActionResult Enviado()
        {
            return View();
        }

    }
}
