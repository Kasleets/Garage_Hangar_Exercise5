
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

      
            // Boundry to comment out for testing purposes, After this comment, main program starts properly.
           
           try
           {

                #region Creation of Configuration Root and reading from appsettings.json file.
                // Create a configuration builder
                var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

               IConfigurationRoot configuration = builder.Build();
                #endregion



                #region Creation of BillingRates Dictionary and reading from appsettings.json file the configuration.
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
                #endregion




                #region Creation of Garage Capacity and reading from appsettings.json file.
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
                #endregion




                #region Creation of garage instance and printing the garage capacity.

                // Create an instance of the garage with the specified capacity by the user or appsettings.json
                Garage<Vehicle> garage = new Garage<Vehicle>(garageCapacity);

                // Print the garage capacity
                Console.WriteLine($"Garage initialized with capacity: {garageCapacity}");
                #endregion




                #region Development of proper program UI and menu.

                var manager = new Manager();
                manager.Run();



                #endregion



            }

            catch (Exception ex)
           #region Exeception Handling, in case of missing or corrupted Appsettings.json file, keep updated.
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
               }
           }  
                   #endregion
            
            // Boundry to comment out for testing purposes
        }
    }
}