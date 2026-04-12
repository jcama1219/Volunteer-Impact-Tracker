using System.Collections.Generic;
using VolunteerImpactTracker.Data;
using VolunteerImpactTracker.Models;

namespace VolunteerImpactTracker.Tests
{
    public class FakeRepository : Repository
    {
        private readonly List<VolunteerHourEntry> _hours = new();
        private readonly List<VolunteerEvent> _events = new();
        private readonly List<ImpactRecord> _impact = new();

        public override void SaveHourEntry(VolunteerHourEntry entry)
        {
            _hours.Add(entry);
        }

        public override List<VolunteerHourEntry> GetAllHourEntries()
        {
            return new List<VolunteerHourEntry>(_hours);
        }

        public override void SaveAllHourEntries(List<VolunteerHourEntry> entries)
        {
            _hours.Clear();
            _hours.AddRange(entries);
        }

        public override void SaveEvent(VolunteerEvent ev)
        {
            _events.Add(ev);
        }

        public override List<VolunteerEvent> GetAllEvents()
        {
            return new List<VolunteerEvent>(_events);
        }

        public override void SaveImpactRecord(ImpactRecord record)
        {
            _impact.Add(record);
        }

        public override List<ImpactRecord> GetAllImpactRecords()
        {
            return new List<ImpactRecord>(_impact);
        }

        public override void SaveAllImpactRecords(List<ImpactRecord> records)
        {
            _impact.Clear();
            _impact.AddRange(records);
        }
    }
}