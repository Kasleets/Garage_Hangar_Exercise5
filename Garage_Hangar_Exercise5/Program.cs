
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
            #region Short testing minimal setup, not main application
            
            // Initialize a Garage instance for testing
            Garage<Vehicle> garage = new Garage<Vehicle>(10);

            // 1. Setup: Create a few sample vehicles
            Car car1 = new Car("ABC123", DateTime.Now, null, 1, 2000, "Petrol", "Toyota");
            Bicycle bike1 = new Bicycle("BIK001", DateTime.Now, null, 0, 0, "Manpower", "Raleigh", "Blue");
            
            // Park these vehicles in the garage
            garage.ParkVehicle(car1);
            garage.ParkVehicle(bike1);
            
            // 2. Execution: List all parked vehicles
            garage.ListAllParkedVehicles();

            var parkedVehicles = garage.GetAllParkedVehicles();
            Console.WriteLine("Parked Vehicles:");
            foreach (var vehicle in parkedVehicles)
            {
                Console.WriteLine($"- {vehicle.Brand}, License Plate: {vehicle.LicensePlate}");
            }



            // 3. Validation: Manually check the console output to ensure the vehicles are listed correctly

            #endregion
            // Boundry to comment out for testing purposes
            /*
           try
           {
               // Create a configuration builder
               var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

               IConfigurationRoot configuration = builder.Build();

               // Load billing rates into the Vehicle's BillingRates dictionary
               Vehicle.InitializeBillingRates(configuration
                   .GetSection("GarageSettings:BillingRates")
                   .Get<Dictionary<string, double>>() ?? new Dictionary<string, double>());


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

               if (garageCapacity <= 0)
               {
                   Console.WriteLine("Warning: Default garage capacity from configuration is invalid. Using a fallback default of 50.");
                   garageCapacity = 50; // A safe default
               }

               Console.WriteLine($"Default garage capacity is: {garageCapacity}");

               // Ask user if they want to specify a custom garage capacity
               // Todo: Consolide custom garage capacity code into a method
               Console.WriteLine("Would you like to specify a custom garage capacity? (yes/no)");
               var garageChoice = Console.ReadLine()!.Trim().ToLower();

               while (garageChoice != "yes" && garageChoice != "no")
               {
                   Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                   Console.WriteLine("Would you like to specify a custom garage capacity? (yes/no)");
                   garageChoice = Console.ReadLine()!.Trim().ToLower();
               }

               if (garageChoice == "yes")
               {
                   bool validInput = false;
                   while (!validInput)
                   {
                       Console.WriteLine("Enter the custom garage capacity:");
                       if (int.TryParse(Console.ReadLine(), out int customCapacity) && customCapacity > 0)
                       {
                           garageCapacity = customCapacity;
                           validInput = true;
                       }
                       else
                       {
                           Console.WriteLine("Invalid capacity. Please enter a positive integer.");
                       }
                   }
               }

               // Create an instance of the garage with the capacity
               Garage<Vehicle> garage = new Garage<Vehicle>(garageCapacity);

               // Print the garage capacity
               Console.WriteLine($"Garage initialized with capacity: {garageCapacity}");
           }

           #region Exeception Handling, keep updated
           catch (Exception ex)
           {
               Console.WriteLine($"An error occurred: {ex.Message}");
               Console.WriteLine("The configuration file is missing or corrupted. What would you like to do?");
               Console.WriteLine("1. Close application");
               Console.WriteLine("2. Use the default values for all vehicles");
               var choice = Console.ReadLine();

               if (choice == "1")
               {
                   Console.WriteLine("Contact the IT-support immediately to resolve the issue.");
                   return; // Closes the application
               }

               else if (choice == "2")
               {
                   Console.WriteLine("Using default values for all vehicles, contact the IT-support immediately to secure the application.");

                   // Initialize BillingRates dictionary with default values
                   Vehicle.InitializeBillingRates(new Dictionary<string, double>
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
                   });

                   // Default garage capacity if not set in the corrupted/missing config
                   int garageCapacity = 50;

                   // Ask user if they want to specify a custom garage capacity
                   Console.WriteLine($"Default Garage Capacity is: {garageCapacity}, Would you like to specify a custom garage capacity? (yes/no)");
                   var garageChoice = Console.ReadLine()!.Trim().ToLower();

                   while (garageChoice != "yes" && garageChoice != "no")
                   {
                       Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                       Console.WriteLine("Would you like to specify a custom garage capacity? (yes/no)");
                       garageChoice = Console.ReadLine()!.Trim().ToLower();
                   }

                   if (garageChoice == "yes")
                   {
                       bool validInput = false;
                       while (!validInput)
                       {
                           Console.WriteLine("Enter the custom garage capacity:");
                           if (int.TryParse(Console.ReadLine(), out int customCapacity) && customCapacity > 0)
                           {
                               garageCapacity = customCapacity;
                               validInput = true;
                           }
                           else
                           {
                               Console.WriteLine("Invalid capacity. Please enter a positive integer.");
                           }
                       }
                   }
                   // If the user enters "no", it simply proceeds with the default value set earlier.
                   #endregion
               }
           }  
            */
            // Boundry to comment out for testing purposes
        }
    }
}