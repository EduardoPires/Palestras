using System;

namespace MeusFilme.API.Models
{
    public class Filme
    {

        public Filme()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public DateTime AnoLancamento { get; set; }
    }
}