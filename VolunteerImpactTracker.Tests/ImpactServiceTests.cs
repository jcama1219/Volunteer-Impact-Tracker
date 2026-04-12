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

        [Fact]
        public void GetImpactSummary_ReturnsGroupedTotals()
        {
            var repo = new FakeRepository();
            var service = new ImpactService(repo);

            service.AddImpactRecord(new ImpactRecord
            {
                Organization = "Food Bank",
                ImpactType = "Families Helped",
                Quantity = 10,
                Date = DateTime.Today
            });

            service.AddImpactRecord(new ImpactRecord
            {
                Organization = "Shelter",
                ImpactType = "Families Helped",
                Quantity = 15,
                Date = DateTime.Today
            });

            var summary = service.GetImpactSummary();

            Assert.True(summary.ContainsKey("Families Helped"));
            Assert.Equal(25, summary["Families Helped"]);
        }

        [Fact]
        public void EditImpactRecord_WithValidData_UpdatesRecord()
        {
            var repo = new FakeRepository();
            var service = new ImpactService(repo);

            service.AddImpactRecord(new ImpactRecord
            {
                Organization = "Old Org",
                ImpactType = "Meals Served",
                Quantity = 10,
                Date = DateTime.Today
            });

            var result = service.EditImpactRecord(
                0,
                "New Org",
                "Families Helped",
                20,
                new DateTime(2026, 4, 1));

            var records = service.GetAllImpactRecords();

            Assert.Equal("Impact record updated successfully.", result);
            Assert.Single(records);
            Assert.Equal("New Org", records[0].Organization);
            Assert.Equal("Families Helped", records[0].ImpactType);
            Assert.Equal(20, records[0].Quantity);
            Assert.Equal(new DateTime(2026, 4, 1), records[0].Date);
        }

        [Fact]
        public void DeleteImpactRecord_WithValidIndex_RemovesRecord()
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

            var result = service.DeleteImpactRecord(0);
            var records = service.GetAllImpactRecords();

            Assert.Equal("Impact record deleted successfully.", result);
            Assert.Empty(records);
        }
    }
}