
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
            // Create a configuration builder
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            // Read the billing rate and capacity from the appsettings.json file
            double billingRate = configuration.GetValue<double>("GarageSettings:BillingRate");
            int garageCapacity = configuration.GetValue<int>("GarageSettings:Capacity");

            // Set the billing rate for the Vehicle class
            Vehicle.BillingRate = billingRate;

            // Print the billing rate
            Console.WriteLine($"Billing rate is: {billingRate}");

            // Create an instance of the garage with the capacity
            Garage<Vehicle> garage = new Garage<Vehicle>(garageCapacity);

            // Print the garage capacity
            Console.WriteLine($"Capacity is: {garageCapacity}");
        }
    }
}