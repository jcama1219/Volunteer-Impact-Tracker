using System;
using System.Collections.Generic;
using System.IO;
using VolunteerImpactTracker.Models;

namespace VolunteerImpactTracker.Data
{
    public class Repository
    {
        private readonly string hoursFile = "hours.txt";
        private readonly string eventsFile = "events.txt";
        private readonly string impactFile = "impact.txt";

        // HOURS

        public virtual void SaveHourEntry(VolunteerHourEntry entry)
        {
            using (StreamWriter writer = new StreamWriter(hoursFile, true))
            {
                writer.WriteLine($"{entry.Date:yyyy-MM-dd}|{entry.Organization}|{entry.Hours}");
            }
        }

        public virtual List<VolunteerHourEntry> GetAllHourEntries()
        {
            var entries = new List<VolunteerHourEntry>();

            if (!File.Exists(hoursFile))
                return entries;

            foreach (var line in File.ReadAllLines(hoursFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');

                if (parts.Length == 3)
                {
                    entries.Add(new VolunteerHourEntry
                    {
                        Date = DateTime.Parse(parts[0]),
                        Organization = parts[1],
                        Hours = double.Parse(parts[2])
                    });
                }
            }

            return entries;
        }

        public virtual void SaveAllHourEntries(List<VolunteerHourEntry> entries)
        {
            using (StreamWriter writer = new StreamWriter(hoursFile, false))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date:yyyy-MM-dd}|{entry.Organization}|{entry.Hours}");
                }
            }
        }

        // EVENTS

        public virtual void SaveEvent(VolunteerEvent ev)
        {
            using (StreamWriter writer = new StreamWriter(eventsFile, true))
            {
                writer.WriteLine($"{ev.EventName}|{ev.Organization}|{ev.EventDate:yyyy-MM-dd}|{ev.StartTime}|{ev.EndTime}");
            }
        }

        public virtual List<VolunteerEvent> GetAllEvents()
        {
            var events = new List<VolunteerEvent>();

            if (!File.Exists(eventsFile))
                return events;

            foreach (var line in File.ReadAllLines(eventsFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');

                if (parts.Length == 5)
                {
                    events.Add(new VolunteerEvent
                    {
                        EventName = parts[0],
                        Organization = parts[1],
                        EventDate = DateTime.Parse(parts[2]),
                        StartTime = TimeSpan.Parse(parts[3]),
                        EndTime = TimeSpan.Parse(parts[4])
                    });
                }
            }

            return events;
        }

        // IMPACT

        public virtual void SaveImpactRecord(ImpactRecord record)
        {
            using (StreamWriter writer = new StreamWriter(impactFile, true))
            {
                writer.WriteLine($"{record.Date:yyyy-MM-dd}|{record.Organization}|{record.ImpactType}|{record.Quantity}");
            }
        }

        public virtual List<ImpactRecord> GetAllImpactRecords()
        {
            var records = new List<ImpactRecord>();

            if (!File.Exists(impactFile))
                return records;

            foreach (var line in File.ReadAllLines(impactFile))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');

                if (parts.Length == 4)
                {
                    records.Add(new ImpactRecord
                    {
                        Date = DateTime.Parse(parts[0]),
                        Organization = parts[1],
                        ImpactType = parts[2],
                        Quantity = int.Parse(parts[3])
                    });
                }
            }

            return records;
        }

        public virtual void SaveAllImpactRecords(List<ImpactRecord> records)
        {
            using (StreamWriter writer = new StreamWriter(impactFile, false))
            {
                foreach (var record in records)
                {
                    writer.WriteLine($"{record.Date:yyyy-MM-dd}|{record.Organization}|{record.ImpactType}|{record.Quantity}");
                }
            }
        }
    }
}