using System;
using System.ComponentModel.DataAnnotations;

namespace MeetupAPI.Models
{
    public class Meetup
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é requerido")]
        [MaxLength(100, ErrorMessage = "O Nome deve possuir no máximo 100 caracteres")]
        [MinLength(3, ErrorMessage = "O Nome deve possuir no mínimo 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Descrição é requerida")]
        [MaxLength(1000, ErrorMessage = "A Descrição deve possuir no máximo 1000 caracteres")]
        [MinLength(10, ErrorMessage = "A Descrição deve possuir no mínimo 10 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A Data é requerida")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O Local é requerido")]
        [MaxLength(150, ErrorMessage = "O Local deve possuir no máximo 150 caracteres")]
        [MinLength(10, ErrorMessage = "O Local deve possuir no mínimo 10 caracteres")]
        public string Local { get; set; }
    }
}