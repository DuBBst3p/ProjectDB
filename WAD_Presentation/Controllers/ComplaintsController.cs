using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Primarie_Craiova.Models;
using Primarie_Craiova.Services.Interfaces;

namespace Primarie_Craiova.Controllers
{
    [Authorize]
    public class ComplaintsController : Controller
    {
        private readonly IComplaintsService _complaintsService;
        private readonly ICitizensService _citizensService;

        public ComplaintsController(IComplaintsService complaintsService, ICitizensService citizensService)
        {
            _complaintsService = complaintsService;
            _citizensService = citizensService;
        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            var complaints = await _complaintsService.GetAllAsync();
            return View(complaints);
        }

        // GET: Complaints/Details/5
        
        public async Task<IActionResult> Details(int id)
        {
            var complaint = await _complaintsService.GetByIdAsync(id);
            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

        // GET: Complaints/Create
        public async Task<IActionResult> Create()
        {
            var citizens = await _citizensService.GetAllAsync();
            ViewData["CitizenID"] = new SelectList(citizens, "CitizenID", "FullName");
            return View();
        }

        // POST: Complaints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                var citizens = await _citizensService.GetAllAsync();
                ViewData["CitizenID"] = new SelectList(citizens, "CitizenID", "FullName", complaint.CitizenID);
                return View(complaint);
            }

            await _complaintsService.CreateAsync(complaint);
            return RedirectToAction(nameof(Index));
        }

        // GET: Complaints/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var complaint = await _complaintsService.GetByIdAsync(id);
            if (complaint == null)
                return NotFound();

            var citizens = await _citizensService.GetAllAsync();
            ViewData["CitizenID"] = new SelectList(citizens, "CitizenID", "FullName", complaint.CitizenID);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, Complaint complaint)
        {
            if (id != complaint.ComplaintID)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var citizens = await _citizensService.GetAllAsync();
                ViewData["CitizenID"] = new SelectList(citizens, "CitizenID", "FullName", complaint.CitizenID);
                return View(complaint);
            }

            var result = await _complaintsService.UpdateAsync(complaint);
            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var complaint = await _complaintsService.GetByIdAsync(id);
            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _complaintsService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
