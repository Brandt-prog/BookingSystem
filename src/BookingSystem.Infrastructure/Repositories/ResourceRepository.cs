using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly AppDbContext _context;

    public ResourceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Resource?> GetByIdAsync(Guid id)
    {
        return await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Resource>> GetAllAsync()
    {
        return await _context.Resources.ToListAsync();
    }

    public async Task AddAsync(Resource resource)
    {
        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Resource resource)
    {
        _context.Resources.Update(resource);
        await _context.SaveChangesAsync();
    }
}