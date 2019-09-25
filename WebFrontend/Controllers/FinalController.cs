using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebFrontend.Controllers
{
    public class FinalController : Controller
    {
        private string[] lines;
        public FinalController()
        {
            lines = System.IO.File.ReadAllLines(Constants.Config.config_path);
        }

        [Route("Enhorabuena")]
        public IActionResult Enhorabuena()
        {
            Random random = new Random();
            var DOMAIN01 = lines[4].Replace("DOMAIN01: ", "");
            var url_par = HttpContext.Session.GetString("url_par");
            url_par = Request.Cookies["idsession"];
            var url = DOMAIN01 + "/mi-participacion?u=" + url_par + "&xa=" + +random.Next();
            ViewBag.url = url;
            return View();
        }

        [Route("Losentimos")]
        public IActionResult Losentimos()
        {
            return View();
        }
    }
}