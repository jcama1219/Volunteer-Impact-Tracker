using System;
using VolunteerImpactTracker.Services;
using Xunit;

namespace VolunteerImpactTracker.Tests
{
    public class VolunteerServiceTests
    {
        [Fact]
        public void LogHours_WithValidInput_ReturnsSuccessMessage()
        {
            var repo = new FakeRepository();
            var service = new VolunteerService(repo);

            var result = service.LogHours("Food Bank", 3);

            Assert.Equal("Hours logged successfully!", result);
        }

        [Fact]
        public void LogHours_WithInvalidHours_ReturnsError()
        {
            var repo = new FakeRepository();
            var service = new VolunteerService(repo);

            var result = service.LogHours("Food Bank", 0);

            Assert.Equal("Error: Hours must be greater than 0.", result);
        }

        [Fact]
        public void GetTotalHours_ReturnsCorrectTotal()
        {
            var repo = new FakeRepository();
            var service = new VolunteerService(repo);

            service.LogHours("Food Bank", 2);
            service.LogHours("Shelter", 3);

            var total = service.GetTotalHours();

            Assert.Equal(5, total);
        }

        [Fact]
        public void GetOrganizationHistory_ReturnsUniqueOrganizations()
        {
            var repo = new FakeRepository();
            var service = new VolunteerService(repo);

            service.LogHours("Food Bank", 2);
            service.LogHours("Shelter", 3);
            service.LogHours("Food Bank", 1);

            var organizations = service.GetOrganizationHistory();

            Assert.Equal(2, organizations.Count);
            Assert.Contains("Food Bank", organizations);
            Assert.Contains("Shelter", organizations);
        }

        [Fact]
        public void GetTotalHoursByDateRange_ReturnsCorrectFilteredTotal()
        {
            var repo = new FakeRepository();
            var service = new VolunteerService(repo);

            repo.SaveHourEntry(new VolunteerImpactTracker.Models.VolunteerHourEntry
            {
                Date = new DateTime(2026, 4, 1),
                Organization = "Food Bank",
                Hours = 2
            });

            repo.SaveHourEntry(new VolunteerImpactTracker.Models.VolunteerHourEntry
            {
                Date = new DateTime(2026, 4, 10),
                Organization = "Shelter",
                Hours = 3
            });

            var total = service.GetTotalHoursByDateRange(
                new DateTime(2026, 4, 1),
                new DateTime(2026, 4, 5));

            Assert.Equal(2, total);
        }

        [Fact]
        public void EditHourEntry_WithValidData_UpdatesEntry()
        {
            var repo = new FakeRepository();
            var service = new VolunteerService(repo);

            service.LogHours("Old Org", 2);

            var result = service.EditHourEntry(0, "New Org", 5);

            var entries = service.GetAllHourEntries();

            Assert.Equal("Volunteer hour entry updated successfully.", result);
            Assert.Single(entries);
            Assert.Equal("New Org", entries[0].Organization);
            Assert.Equal(5, entries[0].Hours);
        }

        [Fact]
        public void EditHourEntry_WithInvalidIndex_ReturnsError()
        {
            var repo = new FakeRepository();
            var service = new VolunteerService(repo);

            var result = service.EditHourEntry(0, "New Org", 5);

            Assert.Equal("Error: Invalid entry selection.", result);
        }
    }
}