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

namespace Teste_Tecnico.Controllers
{
    public class ClienteController : Controller
    {
        private readonly Teste_TecnicoContext _context;

        public ClienteController(Teste_TecnicoContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return _context.ClienteModel != null ?
                        View(await _context.ClienteModel.ToListAsync()) :
                        Problem("Entity set 'Teste_TecnicoContext.ClienteModel'  is null.");
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Email,DataNascimento,CEP")] ClienteModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                //https://viacep.com.br/ws/01001000/json/
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + clienteModel.CEP + "/json/");

                request.AllowAutoRedirect = false;
                HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();

                if (ChecaServidor.StatusCode != HttpStatusCode.OK)
                {
                    //MessageBox.Show("Servidor indisponível");
                    return View(clienteModel); // Sai da rotina
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

                                enderecoModel.Cep = clienteModel.CEP;

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

                                _context.Add(enderecoModel);
                            }
                        }
                    }
                }

                _context.Add(clienteModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteModel);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel.FindAsync(id);
            if (clienteModel == null)
            {
                return NotFound();
            }
            return View(clienteModel);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Email,DataNascimento,CEP")] ClienteModel clienteModel)
        {
            if (id != clienteModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteModelExists(clienteModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clienteModel);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteModel == null)
            {
                return Problem("Entity set 'Teste_TecnicoContext.ClienteModel'  is null.");
            }
            var clienteModel = await _context.ClienteModel.FindAsync(id);
            if (clienteModel != null)
            {
                _context.ClienteModel.Remove(clienteModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteModelExists(int id)
        {
            return (_context.ClienteModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult Pesquisar(string campoPesquisa)
        {
            var clientes = from c in _context.ClienteModel select c;

            if (!string.IsNullOrEmpty(campoPesquisa))
            {
                clientes = clientes.Where(s => s.Nome.Contains(campoPesquisa));
            }

            return View(clientes);
        }
    }
}
