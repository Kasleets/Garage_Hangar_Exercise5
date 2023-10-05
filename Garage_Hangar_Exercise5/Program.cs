
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Configuration.Json;
using Garage_Hangar_Exercise5.Garage_detailed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Garage_Hangar_Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
           

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            Vehicle.BillingRate = configuration.GetValue<double>("GarageSettings:BillingRate");



            var billingRate = configuration.GetValue<double>("GarageSettings:BillingRate");

            Console.WriteLine($"Billing rate is: {billingRate}");


        }
    }
}