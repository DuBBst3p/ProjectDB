using Primarie_Craiova.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primarie_Craiova.Services.Interfaces
{
    public interface IEmployeesService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task CreateAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<List<Department>> GetDepartmentsAsync();
    }
}
