
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
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Default billing rate is: {Vehicle.BillingRates["Default"]}");
                    Console.ResetColor();
               }
               else
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Default billing rate is not specified in the configuration.");
                    Console.ResetColor();
               }
                #endregion




                #region Creation of Garage Capacity and reading from appsettings.json file.
                // Read the garage capacity from the appsettings.json file
                int garageCapacity = configuration.GetValue<int>("GarageSettings:Capacity");

               if (garageCapacity <= 0)
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Warning: Default garage capacity from configuration is invalid. Using a fallback default of 50.");
                    Console.ResetColor();
                   garageCapacity = 50; // A safe default
               }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Default garage capacity is: {garageCapacity}");
                Console.ResetColor();

                // Ask user if they want to specify a custom garage capacity
                // Todo: Consolide custom garage capacity code into a method
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Would you like to specify a custom garage capacity? (yes/no)");
                Console.ResetColor();
               var garageChoice = Console.ReadLine()!.Trim().ToLower();

               while (garageChoice != "yes" && garageChoice != "no")
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Would you like to specify a custom garage capacity? (yes/no)");
                    Console.ResetColor();
                    garageChoice = Console.ReadLine()!.Trim().ToLower();
               }

               if (garageChoice == "yes")
               {
                   bool validInput = false;
                   while (!validInput)
                   {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Enter the custom garage capacity:");
                        Console.ResetColor();
                       if (int.TryParse(Console.ReadLine(), out int customCapacity) && customCapacity > 0)
                       {
                           garageCapacity = customCapacity;
                           validInput = true;
                       }
                       else
                       {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid capacity. Please enter a positive integer.");
                            Console.ResetColor();
                        }
                   }
               }
                #endregion




                #region Creation of garage instance and printing the garage capacity.

                // Create an instance of the garage with the specified capacity by the user or appsettings.json
                Garage<Vehicle> garage = new Garage<Vehicle>(garageCapacity);

                // Print the garage capacity
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Garage initialized with capacity: {garageCapacity}");
                Console.ResetColor();
                #endregion




                #region Development of proper program UI and menu.

                var manager = new Manager(garage);
                manager.Run();



                #endregion



            }

            catch (Exception ex)
           #region Exeception Handling, in case of missing or corrupted Appsettings.json file, keep updated.
           {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine($"An error occurred: {ex.Message}");
               Console.WriteLine("The configuration file is missing or corrupted.");
               Console.ForegroundColor = ConsoleColor.Cyan;
               Console.WriteLine("What would you like to do?");
               Console.WriteLine("1. Close application");
               Console.WriteLine("2. Use the default values for all vehicles");
               Console.ResetColor();

               var choice = Console.ReadLine();

               if (choice == "1")
               {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Contact the IT-support immediately to resolve the issue.");
                    Console.ResetColor();
                   return; // Closes the application
               }

               else if (choice == "2")
               {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Using default values for all vehicles, contact the IT-support immediately to secure the application.");
                    Console.ResetColor();

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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Default Garage Capacity is: {garageCapacity}, Would you like to specify a custom garage capacity? (yes/no)");
                    Console.ResetColor();
                    var garageChoice = Console.ReadLine()!.Trim().ToLower();

                   while (garageChoice != "yes" && garageChoice != "no")
                   {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Would you like to specify a custom garage capacity? (yes/no)");
                        Console.ResetColor();
                        garageChoice = Console.ReadLine()!.Trim().ToLower();
                   }

                   if (garageChoice == "yes")
                   {
                       bool validInput = false;
                       while (!validInput)
                       {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Enter the custom garage capacity:");
                            Console.ResetColor();
                           if (int.TryParse(Console.ReadLine(), out int customCapacity) && customCapacity > 0)
                           {
                               garageCapacity = customCapacity;
                               validInput = true;
                           }
                           else
                           {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid capacity. Please enter a positive integer.");
                                Console.ResetColor();
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