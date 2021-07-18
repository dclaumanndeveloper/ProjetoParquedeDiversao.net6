using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParquedeDiversoes.Data;
using ParquedeDiversoes.Models;

namespace ParquedeDiversoes.Controllers
{
    public class ParquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parques
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parque.ToListAsync());
        }

        // GET: Parques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parque = await _context.Parque
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parque == null)
            {
                return NotFound();
            }

            return View(parque);
        }

        // GET: Parques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,nomeFantasia,razaoSocial,cnpj,qtdBrinquedos,Username")] Parque parque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parque);
        }

        // GET: Parques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parque = await _context.Parque.FindAsync(id);
            if (parque == null)
            {
                return NotFound();
            }
            return View(parque);
        }

        // POST: Parques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nomeFantasia,razaoSocial,cnpj,qtdBrinquedos,Username")] Parque parque)
        {
            if (id != parque.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParqueExists(parque.ID))
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
            return View(parque);
        }

        // GET: Parques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parque = await _context.Parque
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parque == null)
            {
                return NotFound();
            }

            return View(parque);
        }

        // POST: Parques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parque = await _context.Parque.FindAsync(id);
            _context.Parque.Remove(parque);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParqueExists(int id)
        {
            return _context.Parque.Any(e => e.ID == id);
        }
    }
}
