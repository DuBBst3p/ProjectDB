using Microsoft.EntityFrameworkCore;
using Primarie_Craiova.Models;
using Primarie_Craiova.Repositories;
using Primarie_Craiova.Repositories.Interfaces;
using Primarie_Craiova.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primarie_Craiova.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeeRepository;

        public EmployeesService(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllWithDepartmentAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdWithDepartmentAsync(id);
        }

        public async Task CreateAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var existing = await _employeeRepository.GetByIdAsync(employee.EmployeeID);
            if (existing == null)
                return false;

            existing.Name = employee.Name;
            existing.Position = employee.Position;
            existing.DepartmentID = employee.DepartmentID;

            _employeeRepository.Update(existing);
            await _employeeRepository.SaveAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                _employeeRepository.Delete(employee);
                await _employeeRepository.SaveAsync();
            }
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _employeeRepository.GetDepartmentsAsync();
        }
    }
}
