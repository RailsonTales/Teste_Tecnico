using System.ComponentModel.DataAnnotations;

namespace Teste_Tecnico.Models
{
    public class EnderecoModel
    {
        public int ID { get; set; }
                
        public string? Cep { get; set; }
                
        public string? Logradouro { get; set; }

        public string? Complemento { get; set; }
                
        public string? Bairro { get; set; }
                
        public string? Localidade { get; set; }
                
        public string? UF { get; set; }
                
        public string? Numero { get; set; }
    }
}
