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
    }
}