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

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ParkVehicle();
                        break;

                    case "2":
                        RemoveVehicle();
                        break;

                    case "3":
                        SearchAllVehiclesByLicensePlate();
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

                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid input, please try again");
                        break;
                }
            }
        }

        // Creating fake dataset of cars to test functions using Bogus.NuGet package
        private void AddBogusVehicles(int numberOfVehicles = 10)
        {
            #region Old Bogus model, unusable because lacking the parameterless constructor
            //for (int i = 0; i < numberOfVehicles; i++)
            //{
            //    var bogusCar = new Faker<Car>()
            //        .RuleFor(c => c.LicensePlate, f => f.Vehicle.Random.String2(6, 6).ToUpper())
            //        .RuleFor(c => c.EntryTime, f => f.Date.Past(100))
            //        .RuleFor(c => c.ExitTime, f => null)
            //        .RuleFor(c => c.NumberOfEngines, f => f.Random.Int(1, 4))
            //        .RuleFor(c => c.FuelType, f => f.Vehicle.Fuel())
            //        .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
            //        .RuleFor(c => c.Color, f => f.Commerce.Color());

            //    var car = bogusCar.Generate();
            //    _garage.ParkVehicle(car);
            //}
            #endregion

            for (int i = 0; i < numberOfVehicles; i++)
            {
                var bogusCar = new Faker<Car>()
                    .CustomInstantiator(f => new Car(
                        f.Vehicle.Random.String2(6, 6).ToUpper(),                  // LicensePlate
                        f.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now),  // EntryTime
                        null,                                                      // ExitTime
                        f.Random.Int(1, 4),                                        // NumberOfEngines
                        f.Random.Int(1000, 5000),                                  // EngineVolume
                        f.Vehicle.Fuel(),                                          // FuelType
                        f.Vehicle.Manufacturer(),                                  // Brand
                        f.Commerce.Color()                                         // Color
                    ));


                var car = bogusCar.Generate();
                _garage.ParkVehicle(car);
            }

            Console.WriteLine($"\n{numberOfVehicles} cars have been parked successfully!");

            DisplayNumberOfParkedVehicles();

        }

        private void SearchVehiclesByProperties()
        {
            var properties = UI.DisplaySearchMenu();
            var criteria = UI.CaptureCriteria(_garage, properties);
            var vehicles = _garage.FindVehicles(v =>
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
            Console.WriteLine($"Found {vehicles.Count()} vehicles based on the given criteria.");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }
        }

        private void DisplayNumberOfParkedVehicles()
        {
            var allParkedVehicles = _garage.GetAllParkedVehicles();
            Console.WriteLine($"Total number of parked vehicles: {allParkedVehicles.Count()}");
            Console.WriteLine("Amount of leftover parking spaces: " + (_garage.Capacity - allParkedVehicles.Count()));
        }

        private void SearchSpecificVehicleByLicensePlate()
        {
            Console.WriteLine("Enter the license plate of the vehicle you're looking for:");
            string licensePlate = Console.ReadLine();
            var vehicle = _garage.GetVehicle(licensePlate);
            if (vehicle != null)
            {
                Console.WriteLine($"Found vehicle: {vehicle}");
            }
            else
            {
                Console.WriteLine("No vehicle found with the given license plate.");
            }
        }

        private void SearchAllVehiclesByLicensePlate()
        {
            _garage.ListAllParkedVehicles();
        }

        private void RemoveVehicle()
        {
            Console.WriteLine("Enter the license plate of the vehicle to remove:");
            var licensePlate = Console.ReadLine();
            if (_garage.RemoveVehicle(licensePlate!))
            {
                Console.WriteLine("Vehicle removed successfully!");
            }
            else
            {
                Console.WriteLine("Vehicle with the given license plate not found.");
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
                
                 _ => null
            };

            if (vehicleType == null)
            {
                Console.WriteLine("Invalid choice.");
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

                    
                    Console.WriteLine($"Enter {prop.Name}:");
                        input = Console.ReadLine();
                        isValid = ValidateInput(input, prop.PropertyType);
                        if(!isValid)
                        {
                            Console.WriteLine($"Invalid input for {prop.Name}. Please try again.");
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
                Console.WriteLine("Vehicle parked successfully!");
            }
            else if (vehicleToPark == null)
            {
                Console.WriteLine("Failed to create the vehicle. Please try again. You've input null value.");
            }
            else
            {
                Console.WriteLine("Failed to park the vehicle. Garage might be full.");
            }
        }

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
            Console.WriteLine("What kind of Vehicle would you like to park?\n");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Bus");
            Console.WriteLine("3. Truck");
            Console.WriteLine("4. Airplane");
            Console.WriteLine("5. Boat");
            Console.WriteLine("6. Motorcycle");
            Console.WriteLine("7. Bicycle");

        }

        private void ShowMainMeny()
        {
            Console.WriteLine("\nWelcome to Garage Manager, what would you like to do?");
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Remove Vehicle");
            Console.WriteLine("3. Search all cars by their license plate");
            Console.WriteLine("4. Look for a specific car by its license plate ");
            Console.WriteLine("5. See how many vehicles are parked inside a garage");
            Console.WriteLine("6. Look for a specific car based on its specifics");
            Console.WriteLine("0. Close the application");
            Console.WriteLine("'000' Clears the console");
        }


        // ... methods to manage multiple garages
    }

}
