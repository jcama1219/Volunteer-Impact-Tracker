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

        // ---------- HOURS ----------

        public void SaveHourEntry(VolunteerHourEntry entry)
        {
            using (StreamWriter writer = new StreamWriter(hoursFile, true))
            {
                writer.WriteLine($"{entry.Date:yyyy-MM-dd}|{entry.Organization}|{entry.Hours}");
            }
        }

        public List<VolunteerHourEntry> GetAllHourEntries()
        {
            var entries = new List<VolunteerHourEntry>();

            if (!File.Exists(hoursFile))
                return entries;

            foreach (var line in File.ReadAllLines(hoursFile))
            {
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

        // ---------- EVENTS ----------

        public void SaveEvent(VolunteerEvent ev)
        {
            using (StreamWriter writer = new StreamWriter(eventsFile, true))
            {
                writer.WriteLine(
                    $"{ev.EventName}|{ev.Organization}|{ev.EventDate:yyyy-MM-dd}|{ev.StartTime}|{ev.EndTime}"
                );
            }
        }

        public List<VolunteerEvent> GetAllEvents()
        {
            var events = new List<VolunteerEvent>();

            if (!File.Exists(eventsFile))
                return events;

            foreach (var line in File.ReadAllLines(eventsFile))
            {
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
    }
}