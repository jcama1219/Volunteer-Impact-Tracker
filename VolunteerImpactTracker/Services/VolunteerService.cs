using System;
using System.Linq;
using VolunteerImpactTracker.Data;
using VolunteerImpactTracker.Models;

namespace VolunteerImpactTracker.Services
{
    public class VolunteerService
    {
        private readonly Repository _repository;

        public VolunteerService()
        {
            _repository = new Repository();
        }

        // -------- UC1: Log Volunteer Hours --------

        public void LogHours(string organization, double hours)
        {
            if (hours <= 0)
            {
                Console.WriteLine("Error: Hours must be greater than 0.");
                return;
            }

            var entry = new VolunteerHourEntry
            {
                Date = DateTime.Now,
                Organization = organization,
                Hours = hours
            };

            _repository.SaveHourEntry(entry);
            Console.WriteLine("Hours logged successfully!");
        }

        // -------- UC2: View Total Hours --------

        public void ViewTotalHours()
        {
            var entries = _repository.GetAllHourEntries();
            double total = entries.Sum(e => e.Hours);

            Console.WriteLine($"Total Volunteer Hours: {total}");
        }

        // -------- UC4: Add Volunteer Event --------

        public void AddEvent(VolunteerEvent newEvent)
        {
            if (newEvent.EndTime <= newEvent.StartTime)
            {
                Console.WriteLine("Error: End time must be after start time.");
                return;
            }

            var existingEvents = _repository.GetAllEvents();

            foreach (var existing in existingEvents)
            {
                if (existing.EventDate.Date == newEvent.EventDate.Date &&
                    existing.StartTime < newEvent.EndTime &&
                    newEvent.StartTime < existing.EndTime)
                {
                    Console.WriteLine("Conflict detected with an existing event.");
                    return;
                }
            }

            _repository.SaveEvent(newEvent);
            Console.WriteLine("Event saved successfully.");
        }
    }
}