using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFrontend.Data;
using WebFrontend.Models;

namespace WebFrontend.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Seleccioneumatico")]
    public class SeleccioneumaticoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeleccioneumaticoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetResult(String q, String requestNumber)
        {
            var searchText = q;
            var searchData = _context.Llantas.OrderBy(l => l.orden_marca).ThenBy(l => l.literal_marca).ToList();
            if (q != null)
            {
                searchData = _context.Llantas.Where(l => l.literal_marca.Contains(q)).OrderBy(l => l.orden_marca).ThenBy(l => l.literal_marca).ToList();
            }

            return Json(new { searchData, requestNumber });
        }
    }

    [Produces("application/json")]
    [Route("api/FormularioTalleres")]
    public class FormularioTalleresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormularioTalleresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetResult(String q)
        {
            try
            {
                var searchText = q;
                var talleres = _context.Talleres.Where(t => t.pais_tall.Equals("ESPAÑA"));

                if (searchText != null)
                {
                    talleres = talleres.Where(m => m.razonsocial_tall.ToUpper().Contains(searchText)
                                           || m.poblacion_tall.ToUpper().Contains(searchText)
                                           || m.provincia_tall.ToUpper().Contains(searchText)
                                           || m.cp_tall.ToUpper().Contains(searchText)
                                           );
                }
                var searchData = talleres.ToList();
                return Json(new { success = true, searchData = searchData });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
    }
}