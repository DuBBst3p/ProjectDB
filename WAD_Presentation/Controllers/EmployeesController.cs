using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Primarie_Craiova.Models;
using Primarie_Craiova.Services.Interfaces;
using System.Threading.Tasks;


namespace Primarie_Craiova.Controllers
{
    [Authorize (Roles = "Administrator")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = await _employeesService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                employees = employees
                    .Where(e => e.Name.ToLower().Contains(searchString)
                             || e.Position.ToLower().Contains(searchString))
                    .ToList();
            }


            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _employeesService.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _employeesService.GetDepartmentsAsync();
            ViewData["DepartmentID"] = new SelectList(departments, "DepartmentID", "Name");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,Name,Position,DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeesService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            var departments = await _employeesService.GetDepartmentsAsync();
            ViewData["DepartmentID"] = new SelectList(departments, "DepartmentID", "DepartmentID", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _employeesService.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();

            var departments = await _employeesService.GetDepartmentsAsync();
            ViewData["DepartmentID"] = new SelectList(departments, "DepartmentID", "DepartmentID", employee.DepartmentID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,Name,Position,DepartmentID")] Employee employee)
        {
            if (id != employee.EmployeeID) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _employeesService.UpdateAsync(employee);
                if (!success) return NotFound();

                return RedirectToAction(nameof(Index));
            }

            var departments = await _employeesService.GetDepartmentsAsync();
            ViewData["DepartmentID"] = new SelectList(departments, "DepartmentID", "DepartmentID", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _employeesService.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
