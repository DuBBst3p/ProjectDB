using Primarie_Craiova.Models;

public interface IComplaintsRepository
{
    Task<List<Complaint>> GetAllAsync();
    Task<Complaint?> GetByIdAsync(int id);
    Task CreateAsync(Complaint complaint);
    Task<bool> UpdateAsync(Complaint complaint);
    Task<bool> DeleteAsync(int id);
}
