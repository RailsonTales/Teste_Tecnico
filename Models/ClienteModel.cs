using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Teste_Tecnico.Models
{
    public class ClienteModel
    {
        public int ID { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public string CEP { get; set; }
    }
}
