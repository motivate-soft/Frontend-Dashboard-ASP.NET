using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFrontend.Data;
using WebFrontend.Models;

namespace WebFrontend.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Talleres")]
    public class TalleresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TalleresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Talleres
        [HttpPost]
        public IActionResult GetTalleres([FromBody] SearchViewModel model)
        {
            try
            {
                var abuscar = model.abuscar;
                var talleres = _context.Talleres.Where(t => t.pais_tall.Equals("ESPAÑA"));
                talleres = talleres.Where(m => m.razonsocial_tall.ToUpper().Contains(abuscar.ToUpper())
                                        || m.poblacion_tall.ToUpper().Contains(abuscar.ToUpper())
                                        || m.provincia_tall.ToUpper().Contains(abuscar.ToUpper())
                                        || m.cp_tall.ToUpper().Contains(abuscar.ToUpper())
                                        );
                var data = talleres.ToList();
                return Json(new { success = true, data = data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}