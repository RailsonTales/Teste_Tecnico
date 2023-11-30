using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teste_Tecnico.Data;
using Teste_Tecnico.Models;

namespace Teste_Tecnico.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly Teste_TecnicoContext _context;

        public EnderecoController(Teste_TecnicoContext context)
        {
            _context = context;
        }

        // GET: Endereco
        public async Task<IActionResult> Index()
        {
              return _context.EnderecoModel != null ? 
                          View(await _context.EnderecoModel.ToListAsync()) :
                          Problem("Entity set 'Teste_TecnicoContext.EnderecoModel'  is null.");
        }

        // GET: Endereco/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EnderecoModel == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.EnderecoModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // GET: Endereco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Endereco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Cep,Logradouro,Complemento,Bairro,Localidade,UF,Numero")] EnderecoModel enderecoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enderecoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enderecoModel);
        }

        // GET: Endereco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EnderecoModel == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.EnderecoModel.FindAsync(id);
            if (enderecoModel == null)
            {
                return NotFound();
            }
            return View(enderecoModel);
        }

        // POST: Endereco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Cep,Logradouro,Complemento,Bairro,Localidade,UF,Numero")] EnderecoModel enderecoModel)
        {
            if (id != enderecoModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enderecoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoModelExists(enderecoModel.ID))
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
            return View(enderecoModel);
        }

        // GET: Endereco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EnderecoModel == null)
            {
                return NotFound();
            }

            var enderecoModel = await _context.EnderecoModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EnderecoModel == null)
            {
                return Problem("Entity set 'Teste_TecnicoContext.EnderecoModel'  is null.");
            }
            var enderecoModel = await _context.EnderecoModel.FindAsync(id);
            if (enderecoModel != null)
            {
                _context.EnderecoModel.Remove(enderecoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoModelExists(int id)
        {
          return (_context.EnderecoModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
