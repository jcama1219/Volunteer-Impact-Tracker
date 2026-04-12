using System;
using System.Collections.Generic;
using System.Linq;
using VolunteerImpactTracker.Data;
using VolunteerImpactTracker.Models;

namespace VolunteerImpactTracker.Services
{
    public class VolunteerService
    {
        private readonly Repository _repository;

        public VolunteerService() : this(new Repository())
        {
        }

        public VolunteerService(Repository repository)
        {
            _repository = repository;
        }

        public string LogHours(string organization, double hours)
        {
            if (string.IsNullOrWhiteSpace(organization))
                return "Error: Organization is required.";

            if (hours <= 0)
                return "Error: Hours must be greater than 0.";

            var entry = new VolunteerHourEntry
            {
                Date = DateTime.Now,
                Organization = organization,
                Hours = hours
            };

            _repository.SaveHourEntry(entry);
            return "Hours logged successfully!";
        }

        public double GetTotalHours()
        {
            var entries = _repository.GetAllHourEntries();
            return entries.Sum(e => e.Hours);
        }

        public List<string> GetOrganizationHistory()
        {
            var entries = _repository.GetAllHourEntries();

            return entries
                .Select(e => e.Organization)
                .Where(o => !string.IsNullOrWhiteSpace(o))
                .Distinct()
                .OrderBy(o => o)
                .ToList();
        }

        public double GetTotalHoursByDateRange(DateTime startDate, DateTime endDate)
        {
            var entries = _repository.GetAllHourEntries();

            return entries
                .Where(e => e.Date.Date >= startDate.Date && e.Date.Date <= endDate.Date)
                .Sum(e => e.Hours);
        }

        public List<VolunteerHourEntry> GetAllHourEntries()
        {
            return _repository.GetAllHourEntries();
        }

        public string EditHourEntry(int index, string organization, double hours)
        {
            var entries = _repository.GetAllHourEntries();

            if (index < 0 || index >= entries.Count)
                return "Error: Invalid entry selection.";

            if (string.IsNullOrWhiteSpace(organization))
                return "Error: Organization is required.";

            if (hours <= 0)
                return "Error: Hours must be greater than 0.";

            entries[index].Organization = organization;
            entries[index].Hours = hours;

            _repository.SaveAllHourEntries(entries);
            return "Volunteer hour entry updated successfully.";
        }
    }
}