using Primarie_Craiova.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primarie_Craiova.Services.Interfaces
{
    public interface ICitizensService
    {
        Task<List<Citizen>> GetAllAsync();
        Task<Citizen?> GetByIdAsync(int id);
        Task CreateAsync(Citizen citizen);
        Task<bool> UpdateAsync(Citizen citizen);
        Task DeleteAsync(int id);
        Task<List<Citizen>> SearchByNameAsync(string name);

    }
}
