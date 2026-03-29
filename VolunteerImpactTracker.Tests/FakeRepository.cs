using System.Collections.Generic;
using VolunteerImpactTracker.Data;
using VolunteerImpactTracker.Models;

namespace VolunteerImpactTracker.Tests
{
    public class FakeRepository : Repository
    {
        private readonly List<VolunteerHourEntry> _hours = new();
        private readonly List<VolunteerEvent> _events = new();

        public override void SaveHourEntry(VolunteerHourEntry entry)
        {
            _hours.Add(entry);
        }

        public override List<VolunteerHourEntry> GetAllHourEntries()
        {
            return new List<VolunteerHourEntry>(_hours);
        }

        public override void SaveEvent(VolunteerEvent ev)
        {
            _events.Add(ev);
        }

        public override List<VolunteerEvent> GetAllEvents()
        {
            return new List<VolunteerEvent>(_events);
        }
    }
}