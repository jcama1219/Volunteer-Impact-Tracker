namespace VolunteerImpactTracker.Models;

public class VolunteerHourEntry
{
    public DateTime Date { get; set; }
    public string Organization { get; set; } = "";
    public double Hours { get; set; }
    public string Notes { get; set; } = "";
}