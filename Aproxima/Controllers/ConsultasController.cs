using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aproxima.Data;
using Aproxima.Models;
using Aproxima.ViewModel;
using System.Collections.Generic;

namespace Aproxima.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly AproximaContext _context;

        public ConsultasController(AproximaContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index(string id = null)
        {
            var aproximaContext = await _context.Solicitacao
                                                .Include(s => s.Classificacao)
                                                .Where(m => string.IsNullOrEmpty(id) || m.Id == new Guid(id))
                                                .ToListAsync();

            foreach (var item in aproximaContext)
            {
                item.ClassificacaoDescricao = Breadcrumb(item.ClassificacaoId);
            }



            return View(new SolicitacaoViewModel { Solicitacoes = aproximaContext });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Busca")] SolicitacaoViewModel solicitacao)
        {
            var aproximaContext = new List<Solicitacao>();

            if (ModelState.IsValid)
            {
                aproximaContext = await _context.Solicitacao
                                                .Include(s => s.Classificacao)
                                                .Where(m => string.IsNullOrEmpty(solicitacao.Busca) || m.Id == new Guid(solicitacao.Busca))
                                                .ToListAsync();

                foreach (var item in aproximaContext)
                {
                    item.Classificacao.Descricao = Breadcrumb(item.ClassificacaoId);
                }
            }

            return View(new SolicitacaoViewModel { Solicitacoes = aproximaContext });
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacao
                .Include(s => s.Classificacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            ViewData["ClassificacaoId"] = new SelectList(_context.Classificacao, "Id", "Descricao");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassificacaoId,Detalhamento,RendaMensal,AdultosMoramComVoce,AdultosTemRenda,AdultosRenda,Email,Telefone")] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassificacaoId"] = new SelectList(_context.Classificacao, "Id", "Descricao", solicitacao.ClassificacaoId);
            return View(solicitacao);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacao.FindAsync(id);
            if (solicitacao == null)
            {
                return NotFound();
            }
            ViewData["ClassificacaoId"] = new SelectList(_context.Classificacao, "Id", "Descricao", solicitacao.ClassificacaoId);
            return View(solicitacao);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id,ClassificacaoId,Detalhamento,RendaMensal,AdultosMoramComVoce,AdultosTemRenda,AdultosRenda,Email,Telefone")] Solicitacao solicitacao)
        {
            if (id != solicitacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitacaoExists(solicitacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassificacaoId"] = new SelectList(_context.Classificacao, "Id", "Descricao", solicitacao.ClassificacaoId);
            return View(solicitacao);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacao
                .Include(s => s.Classificacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var solicitacao = await _context.Solicitacao.FindAsync(id);
            _context.Solicitacao.Remove(solicitacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitacaoExists(Guid? id)
        {
            return _context.Solicitacao.Any(e => e.Id == id);
        }

        public string Breadcrumb(int id)
        {
            var mensagem = string.Empty;


            while (id != 0)
            {
                var categoria = _context.Classificacao.FirstOrDefault(m => m.Id == id);
                mensagem = categoria.Descricao + " > " + mensagem;
                id = categoria.IdPai;
            }

            return mensagem;

        }
    }
}
