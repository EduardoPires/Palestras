using System;

namespace Domain.Models
{
  public class Cliente
  {
    public Cliente()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
  }
}