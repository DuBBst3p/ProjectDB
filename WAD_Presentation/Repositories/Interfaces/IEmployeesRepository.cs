using Primarie_Craiova.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primarie_Craiova.Repositories.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAllWithDepartmentAsync();
        Task<Employee?> GetByIdWithDepartmentAsync(int id);
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        Task SaveAsync();
        Task<List<Department>> GetDepartmentsAsync();
    }
}
