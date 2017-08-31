using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Context;

namespace Infra.Data.Repository
{
  public class ClienteRepository : IClienteRepository
  {
    private readonly DemoContext _context;

    public ClienteRepository(DemoContext context)
    {
      _context = context;
    }

    public void Adicionar(Cliente cliente)
    {
      _context.Clientes.Add(cliente);
      _context.SaveChanges();
    }

    public IEnumerable<Cliente> ObterTodos()
    {
      return _context.Clientes.ToList();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}