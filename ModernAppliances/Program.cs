using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ModernAppliances
{
    internal class Program
    {
        static List<Appliance> appliances = new List<Appliance>();

        static void Main()
        {
            LoadAppliancesFromFile("appliances.txt");

            while (true)
            {
                Console.WriteLine("Welcome to Modern Appliances!");
                Console.WriteLine("How may we assist you?");
                Console.WriteLine("1 – Check out appliance");
                Console.WriteLine("2 – Find appliances by brand");
                Console.WriteLine("3 – Display appliances by type");
                Console.WriteLine("4 – Produce random appliance list");
                Console.WriteLine("5 – Save & exit");
                Console.Write("Enter option: ");

                int option;
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            CheckOutAppliance();
                            break;
                        case 2:
                            FindAppliancesByBrand();
                            break;
                        case 3:
                            DisplayAppliancesByType();
                            break;
                        case 4:
                            ProduceRandomApplianceList();
                            break;
                        case 5:
                            SaveAppliancesToFile("appliances.txt");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        static void LoadAppliancesFromFile(string filename)
        {
            appliances.Clear();
            string[] lines = File.ReadAllLines(filename);

            foreach (var line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                int itemType = int.Parse(parts[0].Substring(0, 1));
                Appliance appliance = null;

                switch (itemType)
                {
                    case 1:
                        appliance = new Refrigerator(parts);
                        break;
                    case 2:
                        appliance = new Vacuum(parts);
                        break;
                    case 3:
                        appliance = new Microwave(parts);
                        break;
                    case 4:
                    case 5:
                        appliance = new Dishwasher(parts);
                        break;
                    default:
                        Console.WriteLine("Invalid item type: " + itemType);
                        break;
                }

                if (appliance != null)
                {
                    appliances.Add(appliance);
                }
            }
        }

        static void SaveAppliancesToFile(string filename)
        {
            List<string> lines = new List<string>();
            foreach (var appliance in appliances)
            {
                lines.Add(appliance.ToFileString());
            }
            File.WriteAllLines(filename, lines);
        }

        static void CheckOutAppliance()
        {
            Console.Write("Enter the item number of an appliance: ");
            string itemNumber = Console.ReadLine();

            Appliance appliance = appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);
            if (appliance != null && appliance.Quantity > 0)
            {
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                appliance.Quantity--;
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out.");
            }
        }

        static void FindAppliancesByBrand()
        {
            Console.Write("Enter brand to search for: ");
            string brand = Console.ReadLine();

            var matchingAppliances = appliances.Where(a => string.Equals(a.Brand, brand, StringComparison.OrdinalIgnoreCase));
            if (matchingAppliances.Any())
            {
                Console.WriteLine("Matching Appliances:");
                foreach (var appliance in matchingAppliances)
                {
                    Console.WriteLine(appliance);
                }
            }
            else
            {
                Console.WriteLine("No appliances found with that brand.");
            }
        }

        static void DisplayAppliancesByType()
        {
            Console.WriteLine("Appliance Types");
            Console.WriteLine("1 – Refrigerators");
            Console.WriteLine("2 – Vacuums");
            Console.WriteLine("3 – Microwaves");
            Console.WriteLine("4 – Dishwashers");
            Console.Write("Enter type of appliance: ");

            if (int.TryParse(Console.ReadLine(), out int type))
            {
                switch (type)
                {
                    case 1:
                        DisplayRefrigerators();
                        break;
                    case 2:
                        DisplayVacuums();
                        break;
                    case 3:
                        DisplayMicrowaves();
                        break;
                    case 4:
                        DisplayDishwashers();
                        break;
                    default:
                        Console.WriteLine("Invalid appliance type.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid appliance type.");
            }
        }

        static void DisplayRefrigerators()
        {
            Console.Write("Enter number of doors (2, 3, or 4): ");
            if (int.TryParse(Console.ReadLine(), out int numberOfDoors))
            {
                var matchingRefrigerators = appliances.OfType<Refrigerator>().Where(r => r.NumberOfDoors == numberOfDoors);
                if (matchingRefrigerators.Any())
                {
                    Console.WriteLine("Matching refrigerators:");
                    foreach (var refrigerator in matchingRefrigerators)
                    {
                        Console.WriteLine(refrigerator);
                    }
                }
                else
                {
                    Console.WriteLine("No matching refrigerators found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number of doors.");
            }
        }

        static void DisplayVacuums()
        {
            Console.Write("Enter battery voltage (18 or 24): ");
            if (int.TryParse(Console.ReadLine(), out int batteryVoltage))
            {
                var matchingVacuums = appliances.OfType<Vacuum>().Where(v => v.BatteryVoltage == batteryVoltage);
                if (matchingVacuums.Any())
                {
                    Console.WriteLine("Matching vacuums:");
                    foreach (var vacuum in matchingVacuums)
                    {
                        Console.WriteLine(vacuum);
                    }
                }
                else
                {
                    Console.WriteLine("No matching vacuums found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid battery voltage.");
            }
        }

        static void DisplayMicrowaves()
        {
            Console.Write("Enter room type (K for kitchen, W for work site): ");
            string roomType = Console.ReadLine();
            var matchingMicrowaves = appliances.OfType<Microwave>().Where(m => m.RoomType == roomType);
            if (matchingMicrowaves.Any())
            {
                Console.WriteLine("Matching microwaves:");
                foreach (var microwave in matchingMicrowaves)
                {
                    Console.WriteLine(microwave);
                }
            }
            else
            {
                Console.WriteLine("No matching microwaves found.");
            }
        }

        static void DisplayDishwashers()
        {
            Console.Write("Enter sound rating (Qt, Qr, Qu, or M): ");
            string soundRating = Console.ReadLine();
            var matchingDishwashers = appliances.OfType<Dishwasher>().Where(d => d.SoundRating == soundRating);
            if (matchingDishwashers.Any())
            {
                Console.WriteLine("Matching dishwashers:");
                foreach (var dishwasher in matchingDishwashers)
                {
                    Console.WriteLine(dishwasher);
                }
            }
            else
            {
                Console.WriteLine("No matching dishwashers found.");
            }
        }

        static void ProduceRandomApplianceList()
        {
            Console.Write("Enter number of appliances: ");
            if (int.TryParse(Console.ReadLine(), out int numAppliances))
            {
                var random = new Random();
                for (int i = 0; i < numAppliances; i++)
                {
                    int index = random.Next(appliances.Count);
                    Console.WriteLine(appliances[index]);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number of appliances.");
            }
        }
    }
}