using System;
using VolunteerImpactTracker.Models;
using VolunteerImpactTracker.Services;
using Xunit;

namespace VolunteerImpactTracker.Tests
{
    public class ImpactServiceTests
    {
        [Fact]
        public void AddImpactRecord_WithValidInput_ReturnsSuccess()
        {
            var repo = new FakeRepository();
            var service = new ImpactService(repo);

            var result = service.AddImpactRecord(new ImpactRecord
            {
                Organization = "Food Bank",
                ImpactType = "Families Helped",
                Quantity = 25,
                Date = DateTime.Today
            });

            Assert.Equal("Impact record saved successfully.", result);
        }

        [Fact]
        public void AddImpactRecord_WithInvalidQuantity_ReturnsError()
        {
            var repo = new FakeRepository();
            var service = new ImpactService(repo);

            var result = service.AddImpactRecord(new ImpactRecord
            {
                Organization = "Food Bank",
                ImpactType = "Families Helped",
                Quantity = 0,
                Date = DateTime.Today
            });

            Assert.Equal("Error: Quantity must be greater than 0.", result);
        }

        [Fact]
        public void GetAllImpactRecords_ReturnsSavedRecords()
        {
            var repo = new FakeRepository();
            var service = new ImpactService(repo);

            service.AddImpactRecord(new ImpactRecord
            {
                Organization = "Food Bank",
                ImpactType = "Families Helped",
                Quantity = 25,
                Date = DateTime.Today
            });

            var records = service.GetAllImpactRecords();

            Assert.Single(records);
        }
    }
}