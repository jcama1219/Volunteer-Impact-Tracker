using System;
using System.Collections.Generic;
using System.Linq;
using VolunteerImpactTracker.Data;
using VolunteerImpactTracker.Models;

namespace VolunteerImpactTracker.Services
{
    public class EventService
    {
        private readonly Repository _repository;

        public EventService() : this(new Repository())
        {
        }

        public EventService(Repository repository)
        {
            _repository = repository;
        }

        public string AddEvent(VolunteerEvent newEvent)
        {
            if (string.IsNullOrWhiteSpace(newEvent.EventName))
                return "Error: Event name is required.";

            if (string.IsNullOrWhiteSpace(newEvent.Organization))
                return "Error: Organization is required.";

            if (newEvent.EndTime <= newEvent.StartTime)
                return "Error: End time must be after start time.";

            var existingEvents = _repository.GetAllEvents();

            foreach (var existing in existingEvents)
            {
                if (existing.EventDate.Date == newEvent.EventDate.Date &&
                    existing.StartTime < newEvent.EndTime &&
                    newEvent.StartTime < existing.EndTime)
                {
                    return "Conflict detected with an existing event.";
                }
            }

            _repository.SaveEvent(newEvent);
            return "Event saved successfully.";
        }

        public List<VolunteerEvent> GetUpcomingEvents()
        {
            var today = DateTime.Today;

            return _repository.GetAllEvents()
                .Where(e => e.EventDate.Date >= today)
                .OrderBy(e => e.EventDate)
                .ThenBy(e => e.StartTime)
                .ToList();
        }
    }
}