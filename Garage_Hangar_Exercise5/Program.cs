
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Configuration.Json;
using Garage_Hangar_Exercise5.Garage_detailed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types;
using Garage_Hangar_Exercise5.Engine;


namespace Garage_Hangar_Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create a configuration builder
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                // Load billing rates into the Vehicle's BillingRates dictionary
                Vehicle.BillingRates = configuration
                    .GetSection("GarageSettings:BillingRates")
                    .Get<Dictionary<string, double>>() ?? new Dictionary<string, double>();

                // Ensure the "Default" rate exists before trying to print it
                if (Vehicle.BillingRates.ContainsKey("Default"))
                {
                    Console.WriteLine($"Default billing rate is: {Vehicle.BillingRates["Default"]}");
                }
                else
                {
                    Console.WriteLine("Error: Default billing rate is not specified in the configuration.");
                }

                // Read the garage capacity from the appsettings.json file
                int garageCapacity = configuration.GetValue<int>("GarageSettings:Capacity");

                // Create an instance of the garage with the capacity
                Garage<Vehicle> garage = new Garage<Vehicle>(garageCapacity);

                // Print the garage capacity
                Console.WriteLine($"Capacity is: {garageCapacity}");
            }

            // If the configuration file is missing or corrupted, the application will continue with default values for all vehicles if users so wishes
            #region Exception handling, keep updated.
            catch
            {
                Console.WriteLine("The configuration file is missing or corrupted. What would you like to do?");
                Console.WriteLine("1. Close application");
                Console.WriteLine("2. Use the default values for all vehicles");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                Console.WriteLine("Contact the IT-support immediately to resolve the issue.");
                    return; // Closes the application
                }
                if (choice == "2") 
                Console.WriteLine("Using default values for all vehicles, contact the IT-support immediately to secure the application. ");

                // Initialize BillingRates dictionary with default values
                Vehicle.BillingRates = new Dictionary<string, double>
                    {
                    {"Default", 69},
                    {"ElectricChargeRate", 5 },
                    {"Car", 50},
                    {"Truck", 200},
                    {"Airplane", 2000 },
                    {"Bicycle", 3 },
                    {"Boat", 350 },
                    {"Bus", 120 },
                    {"Motorcycle", 30 },

                    // Note: Keep this updated during revisions, and add more default values for other vehicle types here if required
                    };
                #endregion
            }

        }
    }
}
