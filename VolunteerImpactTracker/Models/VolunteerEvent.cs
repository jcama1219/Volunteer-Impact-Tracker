namespace VolunteerImpactTracker.Models
{
    public class VolunteerEvent
    {
        public string EventName { get; set; }
        public string Organization { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
