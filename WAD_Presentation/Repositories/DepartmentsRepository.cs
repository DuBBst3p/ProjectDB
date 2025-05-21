using Microsoft.EntityFrameworkCore;
using Primarie_Craiova.Repositories.Interfaces;

namespace Primarie_Craiova.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync() =>
            await _context.Departments.ToListAsync();

        public async Task<Department?> GetByIdAsync(int id) =>
            await _context.Departments.FindAsync(id);

        public async Task CreateAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }

}
