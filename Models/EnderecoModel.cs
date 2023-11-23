using System.ComponentModel.DataAnnotations;

namespace Teste_Tecnico.Models
{
    public class EnderecoModel
    {
        public int ID { get; set; }

        [Required]
        public string Cep { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Localidade { get; set; }

        [Required]
        public string UF { get; set; }
    }
}
