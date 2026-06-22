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
}