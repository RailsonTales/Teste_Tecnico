using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teste_Tecnico.Data;
using Teste_Tecnico.Models;
using Teste_Tecnico.Services;

namespace Teste_Tecnico.Services
{
    public class Validacoes
    {
        public static string ValidandoCampos(ClienteModel clienteModel, EnderecoModel enderecoModel) 
        {
            if (clienteModel == null || enderecoModel == null)
            {
                return "Nenhum campo obrigatório foi preenchido!";
            }

            if (clienteModel == null || enderecoModel == null)
            {
                return "Nenhum campo obrigatório foi preenchido!";
            }

            if (clienteModel.Email == null)
            {
                return "E-mail vazio!";
            }

            var emailValido = RegexUtilities.IsValidEmail(clienteModel.Email);

            if (!emailValido)
            {
                return "E-mail inválido!";
            }

            if (clienteModel.CEP == null)
            {
                return "CEP vazio!";
            }
            else
            {
                if (clienteModel.CEP.Length != 8)
                    return "CEP inválido!";
                else
                {
                    var enderecoService = new EnderecoService();
                    var enderecoCEPvalido = enderecoService.PesquisarCEP(clienteModel.CEP);
                    if (enderecoCEPvalido == null)
                    {
                        return "CEP inválido!";
                    }
                }
            }

            if(clienteModel.Nome == null)
                return "Nome vazio!";

            if (clienteModel.DataNascimento == null)
                return "Data Nascimento vazio!";

            if (enderecoModel.Logradouro == null)
                return "Logradouro vazio!";

            if (enderecoModel.Bairro == null)
                return "Bairro vazio!";

            if (enderecoModel.Localidade == null)
                return "Localidade vazio!";

            if (enderecoModel.UF == null)
                return "UF vazio!";

            if (enderecoModel.Numero == null)
                return "Número vazio!";

            return "OK";
        }
    }
}
