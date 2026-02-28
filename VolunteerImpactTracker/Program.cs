using System;
using VolunteerImpactTracker.Models;
using VolunteerImpactTracker.Services;

namespace VolunteerImpactTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new VolunteerService();

            while (true)
            {
                Console.WriteLine("\n==== Volunteer Impact Tracker ====");
                Console.WriteLine("1. Log Volunteer Hours");
                Console.WriteLine("2. View Total Volunteer Hours");
                Console.WriteLine("3. Add Volunteer Event");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Organization: ");
                        string org = Console.ReadLine();

                        Console.Write("Hours: ");
                        if (double.TryParse(Console.ReadLine(), out double hours))
                        {
                            service.LogHours(org, hours);
                        }
                        else
                        {
                            Console.WriteLine("Invalid number format.");
                        }
                        break;

                    case "2":
                        service.ViewTotalHours();
                        break;

                    case "3":
                        Console.Write("Event Name: ");
                        string name = Console.ReadLine();

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
                            EventName = name,
                            Organization = eventOrg,
                            EventDate = eventDate,
                            StartTime = start,
                            EndTime = end
                        };

                        service.AddEvent(newEvent);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }
    }
}