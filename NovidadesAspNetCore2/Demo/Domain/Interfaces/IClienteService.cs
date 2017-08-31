using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
  public interface IClienteService : IDisposable
  {
    IEnumerable<Cliente> ObterTodos();
    void Adicionar(Cliente cliente);
  }
}