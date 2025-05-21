using Microsoft.EntityFrameworkCore;
using Primarie_Craiova.Models;

public class ComplaintsRepository : IComplaintsRepository
{
    private readonly ApplicationDbContext _context;

    public ComplaintsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Complaint>> GetAllAsync()
    {
        return await _context.Complaints.Include(c => c.Citizen).ToListAsync();
    }

    public async Task<Complaint?> GetByIdAsync(int id)
    {
        return await _context.Complaints.Include(c => c.Citizen).FirstOrDefaultAsync(c => c.ComplaintID == id);
    }

    public async Task CreateAsync(Complaint complaint)
    {
        _context.Complaints.Add(complaint);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Complaint complaint)
    {
        _context.Complaints.Update(complaint);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var complaint = await _context.Complaints.FindAsync(id);
        if (complaint == null) return false;

        _context.Complaints.Remove(complaint);
        return await _context.SaveChangesAsync() > 0;
    }
}
