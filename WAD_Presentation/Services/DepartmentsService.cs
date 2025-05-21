using Primarie_Craiova.Repositories.Interfaces;
using Primarie_Craiova.Services.Interfaces;

namespace Primarie_Craiova.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDepartmentsRepository _repository;

        public DepartmentsService(IDepartmentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Department>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Department?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<bool> CreateAsync(Department department)
        {
            await _repository.CreateAsync(department);
            return true;
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            var existing = await _repository.GetByIdAsync(department.DepartmentID);
            if (existing == null) return false;

            await _repository.UpdateAsync(department);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }

}
