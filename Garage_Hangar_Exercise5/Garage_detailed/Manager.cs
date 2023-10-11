using Garage_Hangar_Exercise5;
using Garage_Hangar_Exercise5.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types;
using Bogus.DataSets;
using Bogus;
using System.Drawing;
using System.ComponentModel.Design;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    public class Manager
    {
        private Garage<Vehicle> _garage = default!;

        public Manager(Garage<Vehicle> garage)
        {
            _garage = garage;
        }

        

        internal void Run()
        {
            // Adding a test sample of cars to the garage
             AddBogusVehicles();

            bool continueRunning = true;
            while (continueRunning)
            {
                ShowMainMeny();

                //Get input from user

                string input = Console.ReadLine()!;
                switch (input)
                {
                    case "1":
                        ParkVehicle();
                        break;

                    case "2":
                        RemoveVehicle();
                        break;

                    case "3":
                        DisplayAllParkedVehicles();
                        break;

                    case "4":
                        SearchSpecificVehicleByLicensePlate();
                        break;

                    case "5":
                        DisplayNumberOfParkedVehicles();
                        break;

                    case "6":
                        SearchVehiclesByProperties();
                        break;

                    case "000":
                        Console.Clear();
                        break;

                    case "Bogus":
                        AddBogusVehicles();
                        break;

                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please try again");
                        Console.ResetColor();
                        break;
                }
            }
        }

        // Creating fake dataset of cars to test functions using Bogus.NuGet package
        private void AddBogusVehicles(int numberOfVehicles = 10)
        {
            #region Expansion for Bogus model, to include all vehicle types
            //    if (Vehicle_Types == typeof(Bus))
            //    //Add Unique property for Bus
            //    else if (Vehicle_Types == typeof(Truck))
            //    //Add Unique property for Truck

            #endregion

            // Creating a list of data to pick from, to constrict the randomness of the data
            var brands = new List<string> { "Toyota", "Mazda", "Volkswagen", "BMW", "Mercedes", "Audi" };
            var colors = new List<string> {"Red", "Blue", "Pink", "Green", "White", "Black" };
            for (int i = 0; i < numberOfVehicles; i++)
            {
                var bogusCar = new Faker<Car>()
                    .CustomInstantiator(f => new Car(
                        f.Vehicle.Random.String2(6, 6).ToUpper(),                  // LicensePlate
                        f.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now),  // EntryTime
                        null,                                                      // ExitTime
                        f.Random.Int(1, 4),                                        // NumberOfEngines
                        f.Random.Int(10, 50) * 100,                                // EngineVolume
                        f.Vehicle.Fuel(),                                          // FuelType
                        f.PickRandom(brands),                                      // Brand
                        f.PickRandom(colors)                                       // Color
                        ))
                        .FinishWith((f, v) =>
                        {
                            if (v.FuelType == "Electric")
                            {
                                v.EngineVolume = 0;
                            }
                        });

                var car = bogusCar.Generate();
                _garage.ParkVehicle(car);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{numberOfVehicles} cars have been parked successfully!\n");
            Console.ResetColor();

            DisplayNumberOfParkedVehicles();

        }

        private void SearchVehiclesByProperties()
        {
            
            var properties = UI.DisplaySearchMenu();
            var criteria =   UI.CaptureCriteria(_garage, properties);
            var vehicles =   _garage.FindVehicles(v =>
            {
                foreach (var property in criteria.Keys)
                {
                    var value = v.GetType().GetProperty(property)?.GetValue(v)?.ToString();
                    if (!criteria[property].Contains(value))
                    {
                        return false;
                    }
                }
                return true;
            });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Found {vehicles.Count()} vehicles based on the given criteria: {criteria}");
            Console.ResetColor();
            Console.WriteLine("----------------------------------");

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
                Console.WriteLine("----------------------------------");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Found {vehicles.Count()} vehicles based on the given criteria: {criteria}");
            Console.ResetColor();
        }

        private void DisplayNumberOfParkedVehicles()
        {
            var allParkedVehicles = _garage.GetAllParkedVehicles();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Total number of parked vehicles: {allParkedVehicles.Count()}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Amount of leftover parking spaces: " + (_garage.Capacity - allParkedVehicles.Count()));
        }

        private void SearchSpecificVehicleByLicensePlate()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the license plate of the vehicle you're looking for:");
            Console.ResetColor();
            string licensePlate = Console.ReadLine()!;
            var vehicle = _garage.GetVehicle(licensePlate);
            if (vehicle != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Found vehicle: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(vehicle);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No vehicle found with the given license plate.");
                Console.ResetColor();
            }
        }

        private void DisplayAllParkedVehicles()
        {
            _garage.ListAllParkedVehicles();
        }

        private void RemoveVehicle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter the license plate of the vehicle to remove:");
            Console.ResetColor();
            try
            {
                var licensePlate = Console.ReadLine();
               // int? vehicleIndex = _garage.FindVehicleIndex(licensePlate!);
                var vehicle = _garage.GetVehicle(licensePlate!);

                //if (vehicleIndex.HasValue && vehicleIndex != -1)
                if (vehicle != null)
                {
                    vehicle.ExitTime = DateTime.Now;

                    TimeSpan timeParked = vehicle.ExitTime.Value - vehicle.EntryTime;
                    vehicle.LogBilling(timeParked);


                    if (_garage.RemoveVehicle(licensePlate!))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Vehicle removed successfully! \nVehicle with license plate {vehicle.LicensePlate} was billed {vehicle.Cost:F2}.");
                        Console.ResetColor();
                        Logger.LogMovement(vehicle.LicensePlate + " Removed");
                    }
                    else

                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error occurred while trying to remove the vehicle.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vehicle with the given license plate not found.");
                    Console.ResetColor();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return;
            }
            

        }

        private void ParkVehicle()
        {
            ShowParkVehicleMeny();
            var choice = Console.ReadLine();

            // Determine the type of vehicle based on user choice
            Type? vehicleType = choice switch
            {
                "1" => typeof(Car),
                "2" => typeof(Bus),
                "3" => typeof(Truck),
                "4" => typeof(Airplane),
                "5" => typeof(Boat),
                "6" => typeof(Motorcycle),
                "7" => typeof(Bicycle),
                
                
                 _ => null,
                                
            };

            if (choice == "0") // Return to main menu
            {
                return;
            }

            if (vehicleType == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice, going back to Main Menu.");
                Console.ResetColor();
                return;
            }

            // Get properties of the chosen vehicle type
            var properties = vehicleType.GetProperties()
                                        .Where(p => p.Name != "ExitTime" && p.CanWrite) // Exclude ExitTime and read-only properties
                                        .ToList();

            // Use LINQ to capture user input for each property
            var userInput = properties.ToDictionary(
                prop => prop.Name,
                prop =>
                {
                    string input;
                    bool isValid;
                    do
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Enter {prop.Name}:");
                        Console.ResetColor();
                        input = Console.ReadLine()!;
                        isValid = ValidateInput(input!, prop.PropertyType);
                        if(!isValid)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Invalid input for {prop.Name}. Please try again.");
                            Console.ResetColor();
                        }
                        } while (!isValid);
                    return input;
                });


            // Create the vehicle instance using reflection 
            Vehicle? vehicleToPark = choice switch

            {
                // Car
                "1" => (Vehicle?)Activator.CreateInstance(vehicleType,
                                    userInput["LicensePlate"],
                                    DateTime.Now,
                                    null,
                                    int.Parse(userInput["NumberOfEngines"]),
                                    double.Parse(userInput["EngineVolume"]),
                                    userInput["FuelType"],
                                    userInput["Brand"],
                                    userInput["Color"]
                                ),                
                // Bus
                "2" => (Vehicle?)Activator.CreateInstance(vehicleType,
                                    userInput["LicensePlate"],
                                    DateTime.Now,
                                    null,
                                    int.Parse(userInput["NumberOfEngines"]),
                                    double.Parse(userInput["EngineVolume"]),
                                    userInput["FuelType"],
                                    userInput["Brand"],
                                    userInput["Color"],
                                    int.Parse(userInput["NumberOfSeats"])
                                ), 
                // Truck
                "3" => (Vehicle?)Activator.CreateInstance(vehicleType,
                                    userInput["LicensePlate"],
                                    DateTime.Now,
                                    null,
                                    int.Parse(userInput["NumberOfEngines"]),
                                    double.Parse(userInput["EngineVolume"]),
                                    userInput["FuelType"],
                                    userInput["Brand"],
                                    userInput["Color"],
                                    double.Parse(userInput["Length"])
                                ), 
                // Airplane                
                "4" => (Vehicle?)Activator.CreateInstance(vehicleType,
                                    userInput["LicensePlate"],
                                    DateTime.Now,
                                    null,
                                    int.Parse(userInput["NumberOfEngines"]),
                                    double.Parse(userInput["EngineVolume"]),
                                    userInput["FuelType"],
                                    userInput["Brand"],
                                    userInput["Color"],
                                    int.Parse(userInput["NumberOfWings"])
                                ),         
                // Boat
                "5" => (Vehicle?)Activator.CreateInstance(vehicleType,
                                    userInput["LicensePlate"],
                                    DateTime.Now,
                                    null,
                                    int.Parse(userInput["NumberOfEngines"]),
                                    double.Parse(userInput["EngineVolume"]),
                                    userInput["FuelType"],
                                    userInput["Brand"],
                                    userInput["Color"],
                                    int.Parse(userInput["NumberOfFloors"])
                                ),      
                // Motorcycle
                "6" => (Vehicle?)Activator.CreateInstance(vehicleType,
                                    userInput["LicensePlate"],
                                    DateTime.Now,
                                    null,
                                    int.Parse(userInput["NumberOfEngines"]),
                                    double.Parse(userInput["EngineVolume"]),
                                    userInput["FuelType"],
                                    userInput["Brand"],
                                    userInput["Color"],
                                    int.Parse(userInput["StrokeEngine"])
                                ),     
                // Bicycle
                "7" => (Vehicle?)Activator.CreateInstance(vehicleType,
                                    userInput["LicensePlate"],
                                    DateTime.Now,
                                    null,
                                    int.Parse(userInput["NumberOfEngines"]),
                                    double.Parse(userInput["EngineVolume"]),
                                    userInput["FuelType"],
                                    userInput["Brand"],
                                    userInput["Color"],
                                    userInput["Type"]
                                ),
                // ... handle other vehicle types similarly
                _ => null
            };

            if (vehicleToPark != null && _garage.ParkVehicle(vehicleToPark))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Vehicle parked successfully! \n"); // Todo: add more details what were added to the vehicle that parked
                Console.ResetColor();
                Logger.LogMovement(vehicleToPark.LicensePlate + " Parked");
            }
            else if (vehicleToPark == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to create the vehicle. Please try again. You've input null value.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to park the vehicle. Garage might be full.");
                Console.ResetColor();
            }
        }

        // Support method to validate user input for each property
        private bool ValidateInput(string input, Type propertyType)
        {
            if (propertyType == typeof(int))
            {
                return int.TryParse(input, out _);
            }
            else if (propertyType == typeof(double))
            {
                return double.TryParse(input, out _);
            }
            else if (propertyType == typeof(string))
            {
                return !string.IsNullOrWhiteSpace(input);
            }
            
            // Note: expand if needed to support other types
            return true;
        }

        private void ShowParkVehicleMeny()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("What kind of Vehicle would you like to park?\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Bus");
            Console.WriteLine("3. Truck");
            Console.WriteLine("4. Airplane");
            Console.WriteLine("5. Boat");
            Console.WriteLine("6. Motorcycle");
            Console.WriteLine("7. Bicycle");
            Console.WriteLine("0. Return to Main Menu");
            Console.ResetColor();
        }

        private void ShowMainMeny()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nWelcome to Garage Manager, what would you like to do?");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Remove Vehicle");
            Console.WriteLine("3. Display all parked vehicles");
            Console.WriteLine("4. Look for a specific car by its license plate ");
            Console.WriteLine("5. See how many vehicles are parked inside a garage");
            Console.WriteLine("6. Look for a specific car based on its specifics");
            Console.WriteLine("0. Close the application");
            Console.WriteLine("'000' Clears the console");

            Console.ResetColor();
        }


        // ... methods to manage multiple garages
    }

}
