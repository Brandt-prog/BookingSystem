using BookingSystem.Core.Entities;

namespace BookingSystem.Core.Interfaces;

public interface IResourceRepository
{
    Task<Resource?> GetByIdAsync(Guid id);
    Task<List<Resource>> GetAllAsync();
    Task AddAsync(Resource resource);
    Task UpdateAsync(Resource resource);
}