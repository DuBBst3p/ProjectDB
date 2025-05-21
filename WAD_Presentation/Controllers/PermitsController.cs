using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Primarie_Craiova.Controllers
{
    public class PermitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Permits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Permits.Include(p => p.Citizen);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Permits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permit = await _context.Permits
                .Include(p => p.Citizen)
                .FirstOrDefaultAsync(m => m.PermitID == id);
            if (permit == null)
            {
                return NotFound();
            }

            return View(permit);
        }

        // GET: Permits/Create
        public IActionResult Create()
        {
            ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "CitizenID");
            return View();
        }

        // POST: Permits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PermitID,Type,IssueDate,Fee,CitizenID")] Permit permit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "CitizenID", permit.CitizenID);
            return View(permit);
        }

        // GET: Permits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permit = await _context.Permits.FindAsync(id);
            if (permit == null)
            {
                return NotFound();
            }
            ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "CitizenID", permit.CitizenID);
            return View(permit);
        }

        // POST: Permits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PermitID,Type,IssueDate,Fee,CitizenID")] Permit permit)
        {
            if (id != permit.PermitID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermitExists(permit.PermitID))
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
            ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "CitizenID", permit.CitizenID);
            return View(permit);
        }

        // GET: Permits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permit = await _context.Permits
                .Include(p => p.Citizen)
                .FirstOrDefaultAsync(m => m.PermitID == id);
            if (permit == null)
            {
                return NotFound();
            }

            return View(permit);
        }

        // POST: Permits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permit = await _context.Permits.FindAsync(id);
            if (permit != null)
            {
                _context.Permits.Remove(permit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermitExists(int id)
        {
            return _context.Permits.Any(e => e.PermitID == id);
        }
    }
}
