using System;
using VolunteerImpactTracker.Models;
using VolunteerImpactTracker.Services;

namespace VolunteerImpactTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var volunteerService = new VolunteerService();
            var eventService = new EventService();
            var impactService = new ImpactService();

            while (true)
            {
                Console.WriteLine("\n==== Volunteer Impact Tracker ====");
                Console.WriteLine("1. Log Volunteer Hours");
                Console.WriteLine("2. View Total Volunteer Hours");
                Console.WriteLine("3. Add Volunteer Event");
                Console.WriteLine("4. Record Volunteer Impact");
                Console.WriteLine("5. View Organization History");
                Console.WriteLine("6. View Hours by Date Range");
                Console.WriteLine("7. View Upcoming Events");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");

                
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid selection.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Organization: ");
                        string org = Console.ReadLine();

                        Console.Write("Hours: ");
                        if (double.TryParse(Console.ReadLine(), out double hours))
                        {
                            Console.WriteLine(volunteerService.LogHours(org, hours));
                        }
                        else
                        {
                            Console.WriteLine("Invalid number format.");
                        }
                        break;

                    case 2:
                        double total = volunteerService.GetTotalHours();
                        Console.WriteLine($"Total Volunteer Hours: {total}");
                        break;

                    case 3:
                        Console.Write("Event Name: ");
                        string eventName = Console.ReadLine();

                        Console.Write("Organization: ");
                        string eventOrg = Console.ReadLine();

                        Console.Write("Event Date (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime eventDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            break;
                        }

                        Console.Write("Start Time (HH:mm): ");
                        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan start))
                        {
                            Console.WriteLine("Invalid start time.");
                            break;
                        }

                        Console.Write("End Time (HH:mm): ");
                        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan end))
                        {
                            Console.WriteLine("Invalid end time.");
                            break;
                        }

                        var newEvent = new VolunteerEvent
                        {
                            EventName = eventName,
                            Organization = eventOrg,
                            EventDate = eventDate,
                            StartTime = start,
                            EndTime = end
                        };

                        Console.WriteLine(eventService.AddEvent(newEvent));
                        break;

                    case 4:
                        Console.Write("Organization: ");
                        string impactOrg = Console.ReadLine();

                        Console.Write("Impact Type: ");
                        string impactType = Console.ReadLine();

                        Console.Write("Quantity: ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Invalid quantity.");
                            break;
                        }

                        Console.Write("Date (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime impactDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            break;
                        }

                        var impactRecord = new ImpactRecord
                        {
                            Organization = impactOrg,
                            ImpactType = impactType,
                            Quantity = quantity,
                            Date = impactDate
                        };

                        Console.WriteLine(impactService.AddImpactRecord(impactRecord));
                        break;

                    case 5:
                        var organizations = volunteerService.GetOrganizationHistory();

                        if (organizations.Count == 0)
                        {
                            Console.WriteLine("No organization history available.");
                        }
                        else
                        {
                            Console.WriteLine("Organization History:");
                            foreach (var organization in organizations)
                            {
                                Console.WriteLine($"- {organization}");
                            }
                        }
                        break;

                    case 6:
                        Console.Write("Start Date (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            Console.WriteLine("Invalid start date.");
                            break;
                        }

                        Console.Write("End Date (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                        {
                            Console.WriteLine("Invalid end date.");
                            break;
                        }

                        if (endDate < startDate)
                        {
                            Console.WriteLine("End date must be after or equal to start date.");
                            break;
                        }

                        double rangeTotal = volunteerService.GetTotalHoursByDateRange(startDate, endDate);
                        Console.WriteLine($"Total Volunteer Hours from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}: {rangeTotal}");
                        break;

                    case 7:
                        var upcomingEvents = eventService.GetUpcomingEvents();

                        if (upcomingEvents.Count == 0)
                        {
                            Console.WriteLine("No upcoming events found.");
                        }
                        else
                        {
                            Console.WriteLine("Upcoming Events:");
                            foreach (var ev in upcomingEvents)
                            {
                                Console.WriteLine($"{ev.EventDate:yyyy-MM-dd} | {ev.EventName} | {ev.Organization} | {ev.StartTime} - {ev.EndTime}");
                            }
                        }
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }
    }
}
