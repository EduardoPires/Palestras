using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services
{
  public class ClienteService : IClienteService
  {
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
      _clienteRepository = clienteRepository;
    }

    public IEnumerable<Cliente> ObterTodos()
    {
      return _clienteRepository.ObterTodos();
    }

    public void Adicionar(Cliente cliente)
    {
      _clienteRepository.Adicionar(cliente);
    }

    public void Dispose()
    {
      _clienteRepository.Dispose();
    }
  }
}