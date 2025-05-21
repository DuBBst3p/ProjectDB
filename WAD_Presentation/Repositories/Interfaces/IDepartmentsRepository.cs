namespace Primarie_Craiova.Repositories.Interfaces
{
    public interface IDepartmentsRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task CreateAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(int id);
    }

}
