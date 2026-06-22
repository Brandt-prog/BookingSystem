using BookingSystem.Core.Entities;

namespace BookingSystem.Core.Services;

public class BookingValidator
{
    public bool HasOverlap(DateTime existingStart, DateTime existingEnd,
                            DateTime newStart, DateTime newEnd)
    {
        return newStart < existingEnd && newEnd > existingStart;
    }

    public bool IsWithinOpeningHours(Resource resource, DateTime start, DateTime end)
    {
        var startTime = TimeOnly.FromDateTime(start);
        var endTime = TimeOnly.FromDateTime(end);

        return startTime >= resource.OpeningTime && endTime <= resource.ClosingTime;
    }

    public bool IsInFuture(DateTime start, DateTime now)
    {
        return start > now;
    }
}