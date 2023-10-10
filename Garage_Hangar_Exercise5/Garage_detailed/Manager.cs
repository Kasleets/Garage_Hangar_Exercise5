using Garage_Hangar_Exercise5;
using Garage_Hangar_Exercise5.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    public class Manager
    {
        private Garage<Vehicle> garage = default!;



       

        internal void Run()
        {
           // InitGarage();
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

                    case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid input, please try again");
                    break;
            }
        }

        private void SearchVehiclesByProperties()
        {
            var properties = UI.DisplaySearchMenu();
            var criteria = UI.CaptureCriteria(garage, properties);
            var vehicles = garage.FindVehicles(v =>
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
            var allParkedVehicles = garage.GetAllParkedVehicles();
            Console.WriteLine($"Total number of parked vehicles: {allParkedVehicles.Count()}");

        }

        private void SearchSpecificVehicleByLicensePlate()
        {
            Console.WriteLine("Enter the license plate of the vehicle you're looking for:");
            string licensePlate = Console.ReadLine();
            var vehicle = garage.GetVehicle(licensePlate);
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
            garage.ListAllParkedVehicles();
        }

        private void RemoveVehicle()
        {
            throw new NotImplementedException();
        }

        private void ParkVehicle()
        {
            ShowParkVehicleMeny();

            //Get input from user
            //Switch
        }

        private void ShowParkVehicleMeny()
        {
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Buss");

        }

        private void ShowMainMeny()
        {
            Console.WriteLine("Welcome to Garage Manager, what would you like to do?");
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Remove Vehicle");
            Console.WriteLine("3. Search all cars by their license plate");
            Console.WriteLine("4. Look for a specific car by its license plate ");
            Console.WriteLine("5. See how many vehicles are parked inside a garage");
            Console.WriteLine("6. Look for a specific car based on its specifics");
            Console.WriteLine("0. Close the application");
        }

        private void InitGarage()
        {
            //Ask user for capacity
            int capacity = 10;
            garage = new Garage<Vehicle>(capacity);
            
        }

        // ... methods to manage multiple garages
    }

}
