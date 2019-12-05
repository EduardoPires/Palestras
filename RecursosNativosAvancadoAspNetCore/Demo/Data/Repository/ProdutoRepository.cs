using Business;
using Business.Intefaces;
using DevIO.Data.Repository;

namespace Data
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }
      
    }
}