using System;
using System.Threading.Tasks;
using EP.DemoLicoes.UI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EP.DemoLicoes.UI.Extensions
{
    [ViewComponent(Name = "ContadorCurtidas")]
    public class ContadorCurtidas : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ContadorCurtidas(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid produto)
        {
            // A ideia de pegar os likes de uma outra tabela através do produto ID
            // É apenas ilustrativa para demonstrar as possibilidades.

            var likes = await _context.Likes.FirstOrDefaultAsync(c => c.ProdutoId == produto);
            
            return View(likes.Quantidade);
        }
    }
}