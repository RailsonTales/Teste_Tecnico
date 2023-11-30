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
        public async Task<IActionResult> Create([Bind("ID,Nome,Email,DataNascimento,CEP")] ClienteModel clienteModel, EnderecoModel enderecoModel)
        {
            //dsadasd

            var retornoValidacaoCampos = Validacoes.ValidandoCampos(clienteModel, enderecoModel);

            if (retornoValidacaoCampos != "OK")
            {
                TempData["Mensagem"] = retornoValidacaoCampos;
                return View(clienteModel);
            }

            int retornoEndereco = 0;

            _context.Add(enderecoModel);
            retornoEndereco = await _context.SaveChangesAsync();
            var enderecoCliente = await _context.EnderecoModel.ToListAsync();

            if (enderecoCliente != null)
                clienteModel.Endereco = enderecoCliente.LastOrDefault();
            else
                return View(clienteModel);

            if (retornoEndereco == 1)
            {
                _context.Add(clienteModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
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
            
            var enderecoModel = await _context.EnderecoModel.Where(x=>x.ID == clienteModel.EnderecoID).FirstOrDefaultAsync();

            if (enderecoModel == null)
            {
                return NotFound();
            }

            clienteModel.CEP = enderecoModel.Cep;
            clienteModel.Logradouro = enderecoModel.Logradouro;
            clienteModel.Complemento = enderecoModel.Complemento;
            clienteModel.Bairro = enderecoModel.Bairro;
            clienteModel.Localidade = enderecoModel.Localidade;
            clienteModel.UF = enderecoModel.UF;
            clienteModel.Numero = enderecoModel.Numero;

            return View(clienteModel);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Email,DataNascimento,CEP")] ClienteModel clienteModel, EnderecoModel enderecoModel)
        {
            if (id != clienteModel.ID || _context.ClienteModel == null)
            {
                return NotFound();
            }

            try
            {
                var clienteModelBD = await _context.ClienteModel.AsNoTracking().FirstOrDefaultAsync(c => c.ID == id);
                if(clienteModelBD == null)
                    return NotFound();

                var enderecoModelBD = await _context.EnderecoModel.AsNoTracking().FirstOrDefaultAsync(c => c.ID == clienteModelBD.EnderecoID);
                if (enderecoModelBD == null)
                    return NotFound();

                enderecoModelBD.Cep = enderecoModel.Cep;
                enderecoModelBD.Logradouro = enderecoModel.Logradouro;
                enderecoModelBD.Complemento = enderecoModel.Complemento;
                enderecoModelBD.Bairro = enderecoModel.Bairro;
                enderecoModelBD.Localidade = enderecoModel.Localidade;
                enderecoModelBD.UF = enderecoModel.UF;
                enderecoModelBD.Numero = enderecoModel.Numero;

                _context.Update(enderecoModelBD);
                var retornoEnderecoModelBD = await _context.SaveChangesAsync();

                clienteModel.EnderecoID = clienteModelBD.EnderecoID;

                if (retornoEnderecoModelBD == 1)
                {
                    _context.Update(clienteModel);
                    await _context.SaveChangesAsync();
                }
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
            
            //return View(clienteModel);
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

        [HttpPost]
        public JsonResult PesquisarCEP(string campoPesquisaCEP)
        {
            var enderecoService = new EnderecoService();

            var enderecoModel = enderecoService.PesquisarCEP(campoPesquisaCEP);

            return Json(enderecoModel);
        }
    }
}
