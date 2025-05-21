using Microsoft.AspNetCore.Authorization;
using Primarie_Craiova.Models;
using Primarie_Craiova.Repositories;
using Primarie_Craiova.Repositories.Interfaces;
using Primarie_Craiova.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primarie_Craiova.Services
{
    public class CitizensService : ICitizensService
    {
        private readonly ICitizensRepository _citizenRepository;

        public CitizensService(ICitizensRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }

        public async Task<List<Citizen>> GetAllAsync()
        {
            return await _citizenRepository.GetAllAsync();
        }

        public async Task<Citizen?> GetByIdAsync(int id)
        {
            return await _citizenRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Citizen citizen)
        {
            await _citizenRepository.CreateAsync(citizen);
        }

        public async Task<bool> UpdateAsync(Citizen citizen)
        {
            return await _citizenRepository.UpdateAsync(citizen);
        }

        
        public async Task DeleteAsync(int id)
        {
            await _citizenRepository.DeleteAsync(id);
        }

        public async Task<List<Citizen>> SearchByNameAsync(string name)
        {
            return await _citizenRepository.SearchByNameAsync(name);
        }

    }
}
