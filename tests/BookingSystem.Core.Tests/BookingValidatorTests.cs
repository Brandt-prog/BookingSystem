using BookingSystem.Core.Entities;
using BookingSystem.Core.Services;
using Xunit;

namespace BookingSystem.Core.Tests;

public class BookingValidatorTests
{
    private readonly BookingValidator _validator = new();

    [Fact]
    public void HasOverlap_IdenticalTimes_ReturnsTrue()
    {
        var start = new DateTime(2026, 6, 22, 10, 0, 0);
        var end = new DateTime(2026, 6, 22, 11, 0, 0);

        var result = _validator.HasOverlap(start, end, start, end);

        Assert.True(result);
    }

    [Fact]
    public void HasOverlap_NewBookingStartsBeforeAndEndsDuringExisting_ReturnsTrue()
    {
        var existingStart = new DateTime(2026, 6, 22, 10, 0, 0);
        var existingEnd = new DateTime(2026, 6, 22, 12, 0, 0);
        var newStart = new DateTime(2026, 6, 22, 9, 0, 0);
        var newEnd = new DateTime(2026, 6, 22, 11, 0, 0);

        var result = _validator.HasOverlap(existingStart, existingEnd, newStart, newEnd);

        Assert.True(result);
    }

    [Fact]
    public void HasOverlap_NewBookingStartsDuringAndEndsAfterExisting_ReturnsTrue()
    {
        var existingStart = new DateTime(2026, 6, 22, 10, 0, 0);
        var existingEnd = new DateTime(2026, 6, 22, 12, 0, 0);
        var newStart = new DateTime(2026, 6, 22, 11, 0, 0);
        var newEnd = new DateTime(2026, 6, 22, 13, 0, 0);

        var result = _validator.HasOverlap(existingStart, existingEnd, newStart, newEnd);

        Assert.True(result);
    }

    [Fact]
    public void HasOverlap_NewBookingFullyInsideExisting_ReturnsTrue()
    {
        var existingStart = new DateTime(2026, 6, 22, 10, 0, 0);
        var existingEnd = new DateTime(2026, 6, 22, 14, 0, 0);
        var newStart = new DateTime(2026, 6, 22, 11, 0, 0);
        var newEnd = new DateTime(2026, 6, 22, 12, 0, 0);

        var result = _validator.HasOverlap(existingStart, existingEnd, newStart, newEnd);

        Assert.True(result);
    }

    [Fact]
    public void HasOverlap_ExistingFullyInsideNewBooking_ReturnsTrue()
    {
        var existingStart = new DateTime(2026, 6, 22, 11, 0, 0);
        var existingEnd = new DateTime(2026, 6, 22, 12, 0, 0);
        var newStart = new DateTime(2026, 6, 22, 10, 0, 0);
        var newEnd = new DateTime(2026, 6, 22, 14, 0, 0);

        var result = _validator.HasOverlap(existingStart, existingEnd, newStart, newEnd);

        Assert.True(result);
    }

    [Fact]
    public void HasOverlap_NewBookingEndsExactlyWhenExistingStarts_ReturnsFalse()
    {
        var existingStart = new DateTime(2026, 6, 22, 12, 0, 0);
        var existingEnd = new DateTime(2026, 6, 22, 14, 0, 0);
        var newStart = new DateTime(2026, 6, 22, 10, 0, 0);
        var newEnd = new DateTime(2026, 6, 22, 12, 0, 0);

        var result = _validator.HasOverlap(existingStart, existingEnd, newStart, newEnd);

        Assert.False(result);
    }

    [Fact]
    public void HasOverlap_NewBookingStartsExactlyWhenExistingEnds_ReturnsFalse()
    {
        var existingStart = new DateTime(2026, 6, 22, 10, 0, 0);
        var existingEnd = new DateTime(2026, 6, 22, 12, 0, 0);
        var newStart = new DateTime(2026, 6, 22, 12, 0, 0);
        var newEnd = new DateTime(2026, 6, 22, 14, 0, 0);

        var result = _validator.HasOverlap(existingStart, existingEnd, newStart, newEnd);

        Assert.False(result);
    }

    [Fact]
    public void HasOverlap_CompletelySeparateTimes_ReturnsFalse()
    {
        var existingStart = new DateTime(2026, 6, 22, 8, 0, 0);
        var existingEnd = new DateTime(2026, 6, 22, 9, 0, 0);
        var newStart = new DateTime(2026, 6, 22, 15, 0, 0);
        var newEnd = new DateTime(2026, 6, 22, 16, 0, 0);

        var result = _validator.HasOverlap(existingStart, existingEnd, newStart, newEnd);

        Assert.False(result);
    }


    [Fact]
    public void IsWithinOpeningHours_BookingFullyInsideOpeningHours_ReturnsTrue()
    {
        var resource = new Resource
        {
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(22, 0)
        };
        var start = new DateTime(2026, 6, 22, 10, 0, 0);
        var end = new DateTime(2026, 6, 22, 11, 0, 0);

        var result = _validator.IsWithinOpeningHours(resource, start, end);

        Assert.True(result);
    }

    [Fact]
    public void IsWithinOpeningHours_BookingStartsBeforeOpening_ReturnsFalse()
    {
        var resource = new Resource
        {
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(22, 0)
        };
        var start = new DateTime(2026, 6, 22, 7, 0, 0);
        var end = new DateTime(2026, 6, 22, 9, 0, 0);

        var result = _validator.IsWithinOpeningHours(resource, start, end);

        Assert.False(result);
    }

    [Fact]
    public void IsWithinOpeningHours_BookingEndsAfterClosing_ReturnsFalse()
    {
        var resource = new Resource
        {
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(22, 0)
        };
        var start = new DateTime(2026, 6, 22, 21, 0, 0);
        var end = new DateTime(2026, 6, 22, 23, 0, 0);

        var result = _validator.IsWithinOpeningHours(resource, start, end);

        Assert.False(result);
    }

    [Fact]
    public void IsWithinOpeningHours_BookingStartsExactlyAtOpeningTime_ReturnsTrue()
    {
        var resource = new Resource
        {
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(22, 0)
        };
        var start = new DateTime(2026, 6, 22, 8, 0, 0);
        var end = new DateTime(2026, 6, 22, 9, 0, 0);

        var result = _validator.IsWithinOpeningHours(resource, start, end);

        Assert.True(result);
    }

    [Fact]
    public void IsWithinOpeningHours_BookingEndsExactlyAtClosingTime_ReturnsTrue()
    {
        var resource = new Resource
        {
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(22, 0)
        };
        var start = new DateTime(2026, 6, 22, 21, 0, 0);
        var end = new DateTime(2026, 6, 22, 22, 0, 0);

        var result = _validator.IsWithinOpeningHours(resource, start, end);

        Assert.True(result);
    }

    [Fact]
    public void IsInFuture_StartTimeAfterNow_ReturnsTrue()
    {
        var now = new DateTime(2026, 6, 22, 12, 0, 0);
        var start = new DateTime(2026, 6, 23, 10, 0, 0);

        var result = _validator.IsInFuture(start, now);

        Assert.True(result);
    }

    [Fact]
    public void IsInFuture_StartTimeBeforeNow_ReturnsFalse()
    {
        var now = new DateTime(2026, 6, 22, 12, 0, 0);
        var start = new DateTime(2026, 6, 21, 10, 0, 0);

        var result = _validator.IsInFuture(start, now);

        Assert.False(result);
    }

    [Fact]
    public void IsInFuture_StartTimeEqualsNow_ReturnsFalse()
    {
        var now = new DateTime(2026, 6, 22, 12, 0, 0);

        var result = _validator.IsInFuture(now, now);

        Assert.False(result);
    }
}