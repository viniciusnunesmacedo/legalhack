using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aproxima.Data;
using Aproxima.Models;

namespace Aproxima.Controllers
{
    public class SolicitacoesController : Controller
    {
        private readonly AproximaContext _context;

        public SolicitacoesController(AproximaContext context)
        {
            _context = context;
        }

        // GET: Solicitacoes
        public async Task<IActionResult> Index(int id)
        {
            if (id > 0)
            {
                var solicitacao = new Solicitacao { ClassificacaoId = id };
                return View(solicitacao);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,ClassificacaoId,Detalhamento,RendaMensal,AdultosMoramComVoce,AdultosTemRenda,AdultosRenda")] Solicitacao solicitacao)
        {
            var x = _context.Classificacao.FirstOrDefault(m => m.Id == solicitacao.ClassificacaoId);
            solicitacao.Classificacao = x;

            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                solicitacao.Id = Guid.NewGuid();
                _context.Add(solicitacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Documentacoes", new { id = solicitacao.Id });
            }

            return View(solicitacao);
        }


        public async Task<IActionResult> Resumo(string id)
        {
            ViewBag.SolicitacaoId = id;

            return View(new Solicitacao { Id = new Guid(id) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resumo([Bind("Id,Email,Telefone")] Solicitacao solicitacao)
        {
            var x = _context.Solicitacao.FirstOrDefault(m => m.Id == solicitacao.Id);

            x.Email = solicitacao.Email;
            x.Telefone = solicitacao.Telefone;

            _context.Update(x);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");

        }

    }
}
