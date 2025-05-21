using Microsoft.EntityFrameworkCore;
using Primarie_Craiova.Models;
using Primarie_Craiova.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primarie_Craiova.Repositories
{
    public class CitizensRepository : ICitizensRepository
    {
        private readonly ApplicationDbContext _context;

        public CitizensRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Citizen>> GetAllAsync()
        {
            return await _context.Citizens.ToListAsync();
        }

        public async Task<Citizen?> GetByIdAsync(int id)
        {
            return await _context.Citizens.FindAsync(id);
        }

        public async Task CreateAsync(Citizen citizen)
        {
            _context.Citizens.Add(citizen);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Citizen citizen)
        {
            var existing = await _context.Citizens.FindAsync(citizen.CitizenID);
            if (existing == null)
                return false;

            existing.FullName = citizen.FullName;
            existing.Address = citizen.Address;
            existing.PhoneNumber = citizen.PhoneNumber;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen != null)
            {
                _context.Citizens.Remove(citizen);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Citizen>> SearchByNameAsync(string name)
        {
            return await _context.Citizens
                .Where(c => c.FullName != null && c.FullName.Contains(name))
                .ToListAsync();
        }

    }
}
