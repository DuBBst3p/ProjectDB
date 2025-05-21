using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Primarie_Craiova.Models;
using Primarie_Craiova.Services.Interfaces;
using System.Threading.Tasks;

namespace Primarie_Craiova.Controllers
{
    [Authorize]
    public class CitizensController : Controller
    {
        private readonly ICitizensService _citizensService;

        public CitizensController(ICitizensService citizensService)
        {
            _citizensService = citizensService;
        }

        public async Task<IActionResult> Index(string? searchName)
        {
            List<Citizen> citizens;
            if (!string.IsNullOrEmpty(searchName))
            {
                citizens = await _citizensService.SearchByNameAsync(searchName);
            }
            else
            {
                citizens = await _citizensService.GetAllAsync();
            }

            return View(citizens);
        }



      

        // GET: Citizens/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var citizen = await _citizensService.GetByIdAsync(id);
            if (citizen == null)
                return NotFound();

            return View(citizen);
        }

        // GET: Citizens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizenID,FullName,Address,PhoneNumber")] Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                await _citizensService.CreateAsync(citizen);
                return RedirectToAction(nameof(Index));
            }

            return View(citizen);
        }

        // GET: Citizens/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var citizen = await _citizensService.GetByIdAsync(id);
            if (citizen == null)
                return NotFound();

            return View(citizen);
        }

        // POST: Citizens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("CitizenID,FullName,Address,PhoneNumber")] Citizen citizen)
        {
            if (id != citizen.CitizenID)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _citizensService.UpdateAsync(citizen);
                if (!result)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }

            return View(citizen);
        }

        // GET: Citizens/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var citizen = await _citizensService.GetByIdAsync(id);
            if (citizen == null)
                return NotFound();

            return View(citizen);
        }

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _citizensService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
