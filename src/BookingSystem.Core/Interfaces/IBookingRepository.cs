using BookingSystem.Core.Entities;

namespace BookingSystem.Core.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id);
    Task<List<Booking>> GetByResourceIdAsync(Guid resourceId);
    Task<List<Booking>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
}