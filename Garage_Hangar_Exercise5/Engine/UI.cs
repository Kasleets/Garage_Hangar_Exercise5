using Garage_Hangar_Exercise5.Garage_detailed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Engine
{

    public static class UI
    {
        // Todo: Fix the Garage Handler to use the UI properly, and then implement the UI methods and search functions.
        /* ... (other code)

        public static List<string> DisplaySearchMenu()
        {
            Console.WriteLine("\nWhat would you like to search for?");
            List<string> searchableProperties = new List<string> { "Brand", "Color", "FuelType", "NumberOfEngines" }; // We can expand this list later
            for (int i = 0; i < searchableProperties.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {searchableProperties[i]}");
            }
            Console.WriteLine("0. Exit");
            Console.WriteLine("Enter the numbers corresponding to your choices, separated by commas.");

            var input = Console.ReadLine();
            var selectedIndices = input.Split(',').Select(x => int.Parse(x.Trim())).ToList();

            // Filter out invalid indices and map to property names
            var selectedProperties = selectedIndices.Where(index => index > 0 && index <= searchableProperties.Count)
                                                    .Select(index => searchableProperties[index - 1]).ToList();
            return selectedProperties;
        }

        public static Dictionary<string, List<string>> CaptureCriteria(Garage<Vehicle> testGaragePredator, List<string> properties)
        {
            var criteria = new Dictionary<string, List<string>>();
            foreach (var property in properties)
            {
                Console.WriteLine($"\nSelect values for {property}:");
                var options = GetUniqueValuesForProperty(testGaragePredator, property);
                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }
                Console.WriteLine("Enter the numbers corresponding to your choices, separated by commas.");

                var input = Console.ReadLine();
                var selectedIndices = input.Split(',').Select(x => int.Parse(x.Trim())).ToList();

                // Filter out invalid indices and map to actual values
                var selectedValues = selectedIndices.Where(index => index > 0 && index <= options.Count)
                                                    .Select(index => options[index - 1]).ToList();
                criteria[property] = selectedValues;
            }
            return criteria;
        }

        private static List<string> GetUniqueValuesForProperty(Garage<Vehicle> testGaragePredator, string property)
        {
            var values = new HashSet<string>();
            foreach (var vehicle in testGaragePredator)
            {
                var value = vehicle?.GetType().GetProperty(property)?.GetValue(vehicle)?.ToString();
                if (value != null)
                {
                    values.Add(value);
                }
            }
            return values.ToList();
        }
    
        */

    //public static void DisplayMainMenu()
    //{
    //    Console.WriteLine("Welcome to the Garage Manager!");
    //    Console.WriteLine("Please select an option from the menu below:");
    //    Console.WriteLine("1. Park a vehicle");
    //    Console.WriteLine("2. Remove a vehicle");
    //    Console.WriteLine("3. List all vehicles");
    //    Console.WriteLine("4. Search for a vehicle");
    //    Console.WriteLine("5. Exit");
    //}

    //public static void DisplayMainMenu2()
    //{
    //       Console.WriteLine("Welcome to the Garage Manager!");
    //    Console.WriteLine("Please select an option from the menu below:");
    //    Console.WriteLine("1. Park a vehicle");
    //    Console.WriteLine("2. Remove a vehicle");
    //    Console.WriteLine("3. List all vehicles");
    //    Console.WriteLine("4. Search for a vehicle");
    //    Console.WriteLine("5. Exit");
    //}
    /*
    public static List<string> CaptureUserSelection(Dictionary<int, string> options)
    {
        List<int> selectedNumbers = new List<int>();

        Console.WriteLine("Enter the number corresponding to your choice. Type 'done' when finished.");

        while (true) // Keep prompting until 'done' is entered
        {
            var input = Console.ReadLine().Trim();

            if (input.ToLower() == "done")
            {
                if (selectedNumbers.Count > 0)
                {
                    break; // Exit loop if user is done and at least one valid input is given
                }
                else
                {
                    Console.WriteLine("Please select at least one option.");
                    continue; // Continue the loop if no valid input has been given yet
                }
            }

            if (int.TryParse(input, out int number) && options.ContainsKey(number) && !selectedNumbers.Contains(number))
            {
                selectedNumbers.Add(number);
                Console.WriteLine($"Selected {options[number]}. Continue selecting or type 'done' to finish.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number corresponding to your choice.");
            }
        }

        return selectedNumbers.Select(n => options[n]).ToList();
    }

    */



    // Todo: Implement processing of a single vehicle with user input and logging
    /*
    public void ProcessCustomerVehicle()
    {
        // 1. Gather input from the user (e.g., type of vehicle, time parked, etc.)
        var Vehicle = GatherVehicleDetailsFromUser();

        // 2. Calculate billing amount
        var billingAmount = Vehicle.CalculateBillingAmount();

        // 3. Log the billing details
        Vehicle.LogBilling();

        // 4. Present the billing amount to the user or any other post-processing
        DisplayBillingToUser(billingAmount);

        // ... any other steps ...
    }

    private Vehicle GatherVehicleDetailsFromUser()
    {
        // prompts for user details and returns a Vehicle object with the details
        // type of vehicle, time of entry/exit, etc.
    }

    private void DisplayBillingToUser(double amount)
    {
        // Code to display the billing amount to the user
    }
*/
}
}
