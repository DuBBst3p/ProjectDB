using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Primarie_Craiova.Services.Interfaces;

[Authorize(Roles = "Administrator")]
public class DepartmentsController : Controller
{

    private readonly IDepartmentsService _service;


    public DepartmentsController(IDepartmentsService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index() =>
        View(await _service.GetAllAsync());

    public async Task<IActionResult> Details(int id)
    {
        var dept = await _service.GetByIdAsync(id);
        if (dept == null) return NotFound();
        return View(dept);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(department);
            return RedirectToAction(nameof(Index));
        }
        return View(department);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dept = await _service.GetByIdAsync(id);
        if (dept == null) return NotFound();
        return View(dept);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.DepartmentID) return NotFound();

        if (ModelState.IsValid)
        {
            var success = await _service.UpdateAsync(department);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        return View(department);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var dept = await _service.GetByIdAsync(id);
        if (dept == null) return NotFound();
        return View(dept);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}
