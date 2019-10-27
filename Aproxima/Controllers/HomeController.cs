using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aproxima.Models;
using Aproxima.Data;
using Microsoft.EntityFrameworkCore;

namespace Aproxima.Controllers
{
    public class HomeController : Controller
    {
        private readonly AproximaContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AproximaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int id = 0)
        {
            var lista = await _context.Classificacao.Where(m => m.IdPai == id).ToListAsync();

            if (lista.Any())
            {
                return View(lista);
            }
            else
                return RedirectToAction("Index", "Solicitacoes", new { id = id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
