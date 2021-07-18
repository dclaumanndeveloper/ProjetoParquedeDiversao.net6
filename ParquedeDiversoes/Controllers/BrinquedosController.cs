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
    public class BrinquedosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrinquedosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brinquedos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Brinquedo.Include(b => b.parque);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Brinquedos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brinquedo = await _context.Brinquedo
                .Include(b => b.parque)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (brinquedo == null)
            {
                return NotFound();
            }

            return View(brinquedo);
        }

        // GET: Brinquedos/Create
        public IActionResult Create()
        {
            ViewData["parqueId"] = new SelectList(_context.Parque, "ID", "ID");
            return View();
        }

        // POST: Brinquedos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,nome,manutencao,ultimaManutencao,parqueId")] Brinquedo brinquedo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brinquedo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["parqueId"] = new SelectList(_context.Parque, "ID", "ID", brinquedo.parqueId);
            return View(brinquedo);
        }

        // GET: Brinquedos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brinquedo = await _context.Brinquedo.FindAsync(id);
            if (brinquedo == null)
            {
                return NotFound();
            }
            ViewData["parqueId"] = new SelectList(_context.Parque, "ID", "ID", brinquedo.parqueId);
            return View(brinquedo);
        }

        // POST: Brinquedos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nome,manutencao,ultimaManutencao,parqueId")] Brinquedo brinquedo)
        {
            if (id != brinquedo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brinquedo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrinquedoExists(brinquedo.ID))
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
            ViewData["parqueId"] = new SelectList(_context.Parque, "ID", "ID", brinquedo.parqueId);
            return View(brinquedo);
        }

        // GET: Brinquedos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brinquedo = await _context.Brinquedo
                .Include(b => b.parque)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (brinquedo == null)
            {
                return NotFound();
            }

            return View(brinquedo);
        }

        // POST: Brinquedos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brinquedo = await _context.Brinquedo.FindAsync(id);
            _context.Brinquedo.Remove(brinquedo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrinquedoExists(int id)
        {
            return _context.Brinquedo.Any(e => e.ID == id);
        }
    }
}
