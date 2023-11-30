using System.Net;
using System.Text.RegularExpressions;
using Teste_Tecnico.Models;

namespace Teste_Tecnico.Services
{
    public class EnderecoService
    {
        public EnderecoModel PesquisarCEP(string campoPesquisaCEP)
        {
            try
                {
                //https://viacep.com.br/ws/01001000/json/
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + campoPesquisaCEP + "/json/");

                request.AllowAutoRedirect = false;
                HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();

                if (ChecaServidor.StatusCode != HttpStatusCode.OK)
                {
                    //MessageBox.Show("Servidor indisponível");
                    return null; // Sai da rotina
                }

                using (Stream webStream = ChecaServidor.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            response = Regex.Replace(response, "[{},]", string.Empty);
                            response = response.Replace("\"", "");

                            string[] substrings = response.Split('\n');

                            if (substrings.Count() == 12)
                            {
                                EnderecoModel enderecoModel = new EnderecoModel();

                                enderecoModel.Cep = campoPesquisaCEP;

                                string[] logradouro = substrings[2].Split(":".ToCharArray());
                                enderecoModel.Logradouro = logradouro[1];

                                string[] complemento = substrings[3].Split(":".ToCharArray());
                                enderecoModel.Complemento = complemento[1];

                                string[] bairro = substrings[4].Split(":".ToCharArray());
                                enderecoModel.Bairro = bairro[1];

                                string[] localidade = substrings[5].Split(":".ToCharArray());
                                enderecoModel.Localidade = localidade[1];

                                string[] uf = substrings[6].Split(":".ToCharArray());
                                enderecoModel.UF = uf[1];

                                return enderecoModel;
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
