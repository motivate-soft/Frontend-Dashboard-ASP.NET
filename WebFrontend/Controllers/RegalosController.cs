using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFrontend.Data;

namespace WebFrontend.Controllers
{
    public class RegalosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegalosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("regalos")]
        public IActionResult Index()
        {
            var regalos_17_2 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("17") && r.neumaticos.Equals("2")).ToList();
            var regalos_17_4 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("17") && r.neumaticos.Equals("4")).ToList();
            var regalos_18_2 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("18") && r.neumaticos.Equals("2")).ToList();
            var regalos_18_4 = _context.Regalo.Where(r => r.pais.Equals("ES") && r.llantas.Equals("18") && r.neumaticos.Equals("4")).ToList();
            ViewBag.regalos_17_2 = regalos_17_2;
            ViewBag.regalos_17_4 = regalos_17_4;
            ViewBag.regalos_18_2 = regalos_18_2;
            ViewBag.regalos_18_4 = regalos_18_4;
            return View();
        }

        [Route("inicio")]
        public IActionResult Inicio()
        {
            return View();
        }

        // 20
        [Route("regalo-20-amazon")]
        [Route("regalo-20-amazon-seleccion")]
        public IActionResult Amazon20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-casadellibro")]
        [Route("regalo-20-casadellibro-seleccion")]
        public IActionResult Casadellibro20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-corteingles")]
        [Route("regalo-20-corteingles-seleccion")]
        public IActionResult Corteingles20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-decathlon")]
        [Route("regalo-20-decathlon-seleccion")]
        public IActionResult Decathlon20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-fnac")]
        [Route("regalo-20-fnac-seleccion")]
        public IActionResult Fnac20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-sudadera")]
        [Route("regalo-20-sudadera-seleccion")]
        public IActionResult Sudadera20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-20-vivaelcole")]
        [Route("regalo-20-vivaelcole-seleccion")]
        public IActionResult Vivaelcole20()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        // 30
        [Route("regalo-30-amazon")]
        [Route("regalo-30-amazon-seleccion")]
        public IActionResult Amazon30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-casadellibro")]
        [Route("regalo-30-casadellibro-seleccion")]
        public IActionResult Casadellibro30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-corteingles")]
        [Route("regalo-30-corteingles-seleccion")]
        public IActionResult Corteingles30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-decathlon")]
        [Route("regalo-30-decathlon-seleccion")]
        public IActionResult Decathlon30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-fnac")]
        [Route("regalo-30-fnac-seleccion")]
        public IActionResult Fnac30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-mochila")]
        [Route("regalo-30-mochila-seleccion")]
        public IActionResult Mochila30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-30-vivaelcole")]
        [Route("regalo-30-vivaelcole-seleccion")]
        public IActionResult Vivaelcole30()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        // 40
        [Route("regalo-40-amazon")]
        [Route("regalo-40-amazon-seleccion")]
        public IActionResult Amazon40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-casadellibro")]
        [Route("regalo-40-casadellibro-seleccion")]
        public IActionResult Casadellibro40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-corteingles")]
        [Route("regalo-40-corteingles-seleccion")]
        public IActionResult Corteingles40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-decathlon")]
        [Route("regalo-40-decathlon-seleccion")]
        public IActionResult Decathlon40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-fnac")]
        [Route("regalo-40-fnac-seleccion")]
        public IActionResult Fnac40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-chaqueta")]
        [Route("regalo-40-chaqueta-seleccion")]
        public IActionResult Chaqueta40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-40-vivaelcole")]
        [Route("regalo-40-vivaelcole-seleccion")]
        public IActionResult Vivaelcole40()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        // 60
        [Route("regalo-60-amazon")]
        [Route("regalo-60-amazon-seleccion")]
        public IActionResult Amazon60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-casadellibro")]
        [Route("regalo-60-casadellibro-seleccion")]
        public IActionResult Casadellibro60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-corteingles")]
        [Route("regalo-60-corteingles-seleccion")]
        public IActionResult Corteingles60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-decathlon")]
        [Route("regalo-60-decathlon-seleccion")]
        public IActionResult Decathlon60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-fnac")]
        [Route("regalo-60-fnac-seleccion")]
        public IActionResult Fnac60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-altavoz")]
        [Route("regalo-60-altavoz-seleccion")]
        public IActionResult Altavoz60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }

        [Route("regalo-60-vivaelcole")]
        [Route("regalo-60-vivaelcole-seleccion")]
        public IActionResult Vivaelcole60()
        {
            var idsession = HttpContext.Session.GetString("idsession");
            ViewBag.hiddenidsession = idsession; // // send idsession to regalo detail page
            return View();
        }
    }
}
