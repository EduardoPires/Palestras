using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EP.DemoLicoes.UI.Models
{
    public class Likes
    {
        public Likes()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [ForeignKey("Produto")]
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }

        // EF Relation
        public Produto Produto { get; set; }
    }
}