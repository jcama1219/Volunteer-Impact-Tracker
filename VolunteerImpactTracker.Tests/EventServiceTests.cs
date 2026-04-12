using System;
using VolunteerImpactTracker.Models;
using VolunteerImpactTracker.Services;
using Xunit;

namespace VolunteerImpactTracker.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void AddEvent_WithValidEvent_ReturnsSuccess()
        {
            var repo = new FakeRepository();
            var service = new EventService(repo);

            var ev = new VolunteerEvent
            {
                EventName = "Tree Planting",
                Organization = "City Park",
                EventDate = DateTime.Today.AddDays(1),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0)
            };

            var result = service.AddEvent(ev);

            Assert.Equal("Event saved successfully.", result);
        }

        [Fact]
        public void AddEvent_WithEndBeforeStart_ReturnsError()
        {
            var repo = new FakeRepository();
            var service = new EventService(repo);

            var ev = new VolunteerEvent
            {
                EventName = "Tree Planting",
                Organization = "City Park",
                EventDate = DateTime.Today.AddDays(1),
                StartTime = new TimeSpan(11, 0, 0),
                EndTime = new TimeSpan(9, 0, 0)
            };

            var result = service.AddEvent(ev);

            Assert.Equal("Error: End time must be after start time.", result);
        }

        [Fact]
        public void AddEvent_WithConflict_ReturnsConflictMessage()
        {
            var repo = new FakeRepository();
            var service = new EventService(repo);

            service.AddEvent(new VolunteerEvent
            {
                EventName = "First Event",
                Organization = "Org1",
                EventDate = DateTime.Today.AddDays(1),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0)
            });

            var result = service.AddEvent(new VolunteerEvent
            {
                EventName = "Conflict Event",
                Organization = "Org2",
                EventDate = DateTime.Today.AddDays(1),
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0)
            });

            Assert.Equal("Conflict detected with an existing event.", result);
        }
    }
}
