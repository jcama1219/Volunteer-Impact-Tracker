namespace VolunteerImpactTracker.Models
{
    public class ImpactRecord
    {
        public string Organization { get; set; } = "";
        public string ImpactType { get; set; } = "";
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}