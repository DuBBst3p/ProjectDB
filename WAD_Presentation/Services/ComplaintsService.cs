using Primarie_Craiova.Models;

public class ComplaintsService : IComplaintsService
{
    private readonly IComplaintsRepository _complaintsRepository;

    public ComplaintsService(IComplaintsRepository complaintsRepository)
    {
        _complaintsRepository = complaintsRepository;
    }

    public Task<List<Complaint>> GetAllAsync() => _complaintsRepository.GetAllAsync();

    public Task<Complaint?> GetByIdAsync(int id) => _complaintsRepository.GetByIdAsync(id);

    public Task CreateAsync(Complaint complaint) => _complaintsRepository.CreateAsync(complaint);

    public Task<bool> UpdateAsync(Complaint complaint) => _complaintsRepository.UpdateAsync(complaint);

    public Task<bool> DeleteAsync(int id) => _complaintsRepository.DeleteAsync(id);
}
