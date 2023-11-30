using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teste_Tecnico.Models
{
    public class ClienteModel
    {
        public int ID { get; set; }
        
        public string? Nome { get; set; }
                
        public string? Email { get; set; }
                
        public DateTime? DataNascimento { get; set; }
                
        public string? CEP { get; set; }

        public int EnderecoID { get; set; }
        public EnderecoModel? Endereco { get; set; }
        [NotMapped]
        public string? Logradouro { get; set; }
        [NotMapped]
        public string? Complemento { get; set; }
        [NotMapped]
        public string? Bairro { get; set; }
        [NotMapped]
        public string? Localidade { get; set; }
        [NotMapped]
        public string? UF { get; set; }
        [NotMapped]
        public string? Numero { get; set; }
    }
}
