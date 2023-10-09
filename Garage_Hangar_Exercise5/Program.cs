
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Configuration.Json;
using Garage_Hangar_Exercise5.Garage_detailed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Garage<Vehicle> testGaragePredator = new Garage<Vehicle>(10);

            // 1. Setup: Create a few sample vehicles
            Car car1 = new Car("ABC123", DateTime.Now, null, 1, 2000, "Petrol", "Toyota", "Red");
            Bicycle bike1 = new Bicycle("BIK001", DateTime.Now, null, 0, 0, "Manpower", "Raleigh", "Blueoni","Pink");
            
            // Park these vehicles in the garage
            testGaragePredator.ParkVehicle(car1);
            testGaragePredator.ParkVehicle(bike1);
            testGaragePredator.ParkVehicle(new Car("KOI123", DateTime.Now, null, 1, 2000, "Petrol", "Toyota", "BluE"));
            testGaragePredator.ParkVehicle(new Car("PLU123", DateTime.Now, null, 1, 6200, "Petrol", "ToyOTa", "ReD"));
            testGaragePredator.ParkVehicle(new Car("UTI123", DateTime.Now, null, 1, 3900, "Petrol", "Mazda", "Black"));
            testGaragePredator.ParkVehicle(new Car("XYZ456", DateTime.Now, null, 1, 2500, "Petrol", "Ford", "Blue"));
            testGaragePredator.ParkVehicle(new Truck("LMN789", DateTime.Now, null, 2, 5000, "Diesel", "Volvo", 14000, "Red"));

            //// 2. Execution: List all parked vehicles
            //Console.WriteLine("Another new implementation \nMethod for GetAllParkedVehicles()\n");
            //testGaragePredator.ListAllParkedVehicles();

            //var parkedVehicles = testGaragePredator.GetAllParkedVehicles();
            //Console.WriteLine("\nAnother new implementation \nParked Vehicles:");
            //foreach (var vehicle in parkedVehicles)
            //{
            //    Console.WriteLine($"- {vehicle.Brand}, License Plate: {vehicle.LicensePlate}");
            //}

            //Console.WriteLine("\nAnother new implementation \nTrying out GetVehicle method, a vehicle with ABC123 license plate");
            //Console.WriteLine(testGaragePredator.GetVehicle("ABC123"));

            //Console.WriteLine("\nAnother new implementation \nDisplaying the capacity method/properties");

            //Console.WriteLine($"\nGarage Capacity: {testGaragePredator.Capacity}");
            //Console.WriteLine($"Available Slots in Garage: {testGaragePredator.AvailableSlots}");
            //Console.WriteLine($"Is Garage Full: {testGaragePredator.IsFull}");

            //// Trying out the search functions
            //Console.WriteLine("\nAnother new implementation \nTrying out searching by type function");
            //var blackCars = testGaragePredator.FindVehicles(v => v.Color == "Black" && v is Car);
            //Console.WriteLine("Black Cars:");
            //foreach (var car in blackCars)
            //{
            //    Console.WriteLine(car);
            //}

            //Console.WriteLine("\nAnother new implementation \nTrying out searching by brand Toyota");
            //var toyotaVehicles = testGaragePredator.FindVehicles(v => v.Brand == "Toyota");
            //Console.WriteLine("\nToyota Vehicles:");
            //foreach (var vehicle in toyotaVehicles)
            //{
            //    Console.WriteLine(vehicle);
            //}

         #region Search Logic Testing

            // Use the SearchSpecificationBuilder to construct the predicate
            var builder = new SearchSpecificationBuilder<Vehicle>();
            builder.AddCriteria(v => v.Brand.Equals("Toyota", StringComparison.OrdinalIgnoreCase));
            builder.AddCriteria(v => v.Color.Equals("Red", StringComparison.OrdinalIgnoreCase));
            var predicate = builder.Build(); 

            // New Search logic, more advanced
            // Find vehicles and print results
            Console.WriteLine("\nSearching for all Toyotas that are Red (case-insensitive):");
            var matchedVehicles = testGaragePredator.FindVehicles(predicate); // Don't forget to compile the predicate
            foreach (var vehicle in matchedVehicles)
            {
                Console.WriteLine(vehicle);
            }

            #region bloated, old legacy search logic, missing a part, redundant.
            //// Simple Search
            //Console.WriteLine("Simple Search: All Toyotas");
            //var toyotaVehicles = testGaragePredator.FindVehicles(v => v.Brand == "Toyota");
            //foreach (var vehicle in toyotaVehicles)
            //{
            //    Console.WriteLine(vehicle);
            //}

            //// Compound Search
            //Console.WriteLine("\nCompound Search: All Red Toyotas");
            //var redToyotas = testGaragePredator.FindVehicles(v => v.Brand == "Toyota" && v.Color == "Red");
            //foreach (var vehicle in redToyotas)
            //{
            //    Console.WriteLine(vehicle);
            //}

            //// No Matches
            //Console.WriteLine("\nNo Matches: All Blue Ferraris");
            //var blueFerraris = testGaragePredator.FindVehicles(v => v.Brand == "Ferrari" && v.Color == "Blue");
            //foreach (var vehicle in blueFerraris)
            //{
            //    Console.WriteLine(vehicle);
            //}

            //// Complex Search
            //Console.WriteLine("\nComplex Search: All cars with engine volume more than 3000 and are of Petrol fuel type");
            //var complexSearch = testGaragePredator.FindVehicles(v => v is Car && v.EngineVolume > 3000 && v.FuelType == "Petrol");
            //foreach (var vehicle in complexSearch)
            //{
            //    Console.WriteLine(vehicle);
            //}
            #endregion

            #endregion

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