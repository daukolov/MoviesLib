using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportLeagueASPNetCoreTest.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Обработка 404 ошибки
        /// </summary>
        public IActionResult StatusCode404()
        {
            return View(viewName: "NotFound");
        }

        /// <summary>
        /// Обработка 403 ошибки
        /// </summary>
        public IActionResult StatusCode403()
        {
            return View("Forbiden");
        }
    }
}
