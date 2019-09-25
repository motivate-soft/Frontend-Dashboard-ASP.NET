using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebFrontend.Controllers
{
    public class TalleresController : Controller
    {
        [Route("talleres")]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
