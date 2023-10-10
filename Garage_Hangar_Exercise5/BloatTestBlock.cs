using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5
{
    internal class BloatTestBlock
    {
        #region Working garage appsettings setup
        //        //// 2. Execution: List all parked vehicles
        //        //Console.WriteLine("Another new implementation \nMethod for GetAllParkedVehicles()\n");
        //        //testGaragePredator.ListAllParkedVehicles();

        //        //var parkedVehicles = testGaragePredator.GetAllParkedVehicles();
        //        //Console.WriteLine("\nAnother new implementation \nParked Vehicles:");
        //        //foreach (var vehicle in parkedVehicles)
        //        //{
        //        //    Console.WriteLine($"- {vehicle.Brand}, License Plate: {vehicle.LicensePlate}");
        //        //}

        //        //Console.WriteLine("\nAnother new implementation \nTrying out GetVehicle method, a vehicle with ABC123 license plate");
        //        //Console.WriteLine(testGaragePredator.GetVehicle("ABC123"));

        //        //Console.WriteLine("\nAnother new implementation \nDisplaying the capacity method/properties");

        //        //Console.WriteLine($"\nGarage Capacity: {testGaragePredator.Capacity}");
        //        //Console.WriteLine($"Available Slots in Garage: {testGaragePredator.AvailableSlots}");
        //        //Console.WriteLine($"Is Garage Full: {testGaragePredator.IsFull}");


        #endregion



        #region Sampling the UI search options
        //        //// Simple Search
        //        //Console.WriteLine("Simple Search: All Toyotas");
        //        //var toyotaVehicles = testGaragePredator.FindVehicles(v => v.Brand == "Toyota");
        //        //foreach (var vehicle in toyotaVehicles)
        //        //{
        //        //    Console.WriteLine(vehicle);
        //        //}


        //        //    //Todo: Fix Garage handler and then Implement the UI methods and search functions.
        //        //    /*
        //        //     Advanced user UI search
        //        //    Console.WriteLine("Welcome to Vehicle Search!");
        //        //    var selectedProperties = UI.DisplaySearchMenu();
        //        //    if (selectedProperties.Count == 0)
        //        //    {
        //        //        Console.WriteLine("Exiting the search.");
        //        //        return;
        //        //    }
        //        //    var searchCriteria = UI.CaptureCriteria(testGaragePredator, selectedProperties);
        //        //    */

        //        //    // Sample Dictionary for vehicle colors
        //        //    Dictionary<int, string> vehicleColors = new Dictionary<int, string>
        //        //   {
        //        //        { 1, "Red" },
        //        //        { 2, "Blue" },
        //        //        { 3, "Green" },
        //        //        { 4, "Black" }
        //        //   };

        //        //    // Display options to the user
        //        //    Console.WriteLine("\nSelect vehicle colors:");
        //        //    foreach (var item in vehicleColors)
        //        //    {
        //        //        Console.WriteLine($"{item.Key}. {item.Value}");
        //        //    }

        //        //// Capture user selection
        //        //var selectedColors = UI.CaptureUserSelection(vehicleColors);

        //        //// Display the selected options to the user
        //        //Console.WriteLine("\nYou selected:");
        //        //    foreach (var color in selectedColors)
        //        //    {
        //        //        Console.WriteLine(color);
        //        //    }

        //        //// Trying out the search functions
        //        //Console.WriteLine("\nAnother new implementation \nTrying out searching by type function");
        //        //var blackCars = testGaragePredator.FindVehicles(v => v.Color == "Black" && v is Car);
        //        //Console.WriteLine("Black Cars:");
        //        //foreach (var car in blackCars)
        //        //{
        //        //    Console.WriteLine(car);
        //        //}


        //        //Console.WriteLine("\nAnother new implementation \nTrying out searching by brand Toyota");
        //        //var toyotaVehicles = testGaragePredator.FindVehicles(v => v.Brand == "Toyota");
        //        //Console.WriteLine("\nToyota Vehicles:");
        //        //foreach (var vehicle in toyotaVehicles)
        //        //{
        //        //    Console.WriteLine(vehicle);
        //        //}



        #endregion

        #region Iteration of searching methods
        //        //// New Search logic, more advanced
        //        //// Find vehicles and print results
        //        //Console.WriteLine("\nSearching for all Toyotas that are Red (case-insensitive):");
        //        //var matchedVehicles = testGaragePredator.FindVehicles(predicate); // Don't forget to compile the predicate
        //        //foreach (var vehicle in matchedVehicles)
        //        //{
        //        //    Console.WriteLine(vehicle);
        //        //}
        #endregion


        #region Worked with Dimitris on this, will try to implement to main program directly

        //        //Create manager
        //        var manager = new Manager();
        //        manager.Run();
        #region Short testing minimal setup, not main application

        //        // Initialize a Garage instance for testing
        //        Garage<Vehicle> testGaragePredator = new Garage<Vehicle>(10);

        //        // 1. Setup: Create a few sample vehicles
        //        Car car1 = new Car("ABC123", DateTime.Now, null, 1, 2000, "Petrol", "Toyota", "Red");
        //        Bicycle bike1 = new Bicycle("BIK001", DateTime.Now, null, 0, 0, "Manpower", "Raleigh", "Blueoni", "Pink");

        //        // Park these vehicles in the garage
        //        testGaragePredator.ParkVehicle(car1);
        //        testGaragePredator.ParkVehicle(bike1);
        //        testGaragePredator.ParkVehicle(new Car("KOI123", DateTime.Now, null, 1, 2000, "Petrol", "Toyota", "BluE"));
        //        testGaragePredator.ParkVehicle(new Car("PLU123", DateTime.Now, null, 1, 6200, "Petrol", "ToyOTa", "ReD"));
        //        testGaragePredator.ParkVehicle(new Car("UTI123", DateTime.Now, null, 1, 3900, "Petrol", "Mazda", "Black"));
        //        testGaragePredator.ParkVehicle(new Car("XYZ456", DateTime.Now, null, 1, 2500, "Petrol", "Ford", "Blue"));
        //        testGaragePredator.ParkVehicle(new Truck("LMN789", DateTime.Now, null, 2, 5000, "Diesel", "Volvo", 14000, "Red"));





        #region bloated

        //        // Compound Search
        //        Console.WriteLine("\nCompound Search: All Red Toyotas");

        //        var car = new Car("X", DateTime.Now, null, 1, 1, "X", "X", "Red");
        //        var search = "Buss";

        //        IEnumerable<Vehicle> res = search == "all" ? testGaragePredator : testGaragePredator.Where(v => v.GetType().Name == search);    

        //        if(car.LicensePlate != "X")
        //        {
        //            res = testGaragePredator.Where(v => v.LicensePlate.Contains(car.LicensePlate));
        //        }  
        //        if(car.Brand != "X")
        //        {
        //            res = res.Where(v => v.Brand == car.Brand);
        //        }
        //if (car.Color != "X")
        //{
        //    res = res.Where(v => v.Color == car.Color);
        //}

        //foreach (var vehicle in res)
        //{
        //    Console.WriteLine(vehicle);
        //}


        #endregion


        #endregion



    }
}
#endregion