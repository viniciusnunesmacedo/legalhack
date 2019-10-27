using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aproxima.Data;

namespace Aproxima.Controllers
{
    public class DocumentacoesController : Controller
    {
        private readonly AproximaContext _context;

        public DocumentacoesController(AproximaContext context)
        {
            _context = context;
        }

        // GET: Documentacoes
        public async Task<IActionResult> Index(string id)
        {
            var solicitacao = _context.Solicitacao.FirstOrDefault(m => m.Id.ToString() == id);
            var aproximaContext = _context.Documentacao.Where(m=>m.ClassificacaoId == solicitacao.ClassificacaoId);
            ViewBag.SolicitacaoId = solicitacao.Id;

            return View(await aproximaContext.ToListAsync());
        }
    }
}
