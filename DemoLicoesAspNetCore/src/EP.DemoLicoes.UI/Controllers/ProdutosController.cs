using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EP.DemoLicoes.UI.Data;
using EP.DemoLicoes.UI.Extensions;
using EP.DemoLicoes.UI.Models;

namespace EP.DemoLicoes.UI.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.AsNoTracking().Include("Likes").ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.AsNoTracking().Include("Likes").FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [ClaimsAuthorize("Produto", "Incluir")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ClaimsAuthorize("Produto","Incluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Valor,Ativo")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.Id = Guid.NewGuid();
                _context.Add(produto);
                _context.Likes.Add(new Likes {ProdutoId = produto.Id, Quantidade = 0});

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        [ClaimsAuthorize("Produto", "Editar")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost, ClaimsAuthorize("Produto", "Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Descricao,Valor,Ativo")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
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
            return View(produto);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
