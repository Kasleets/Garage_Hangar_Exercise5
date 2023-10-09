using Bogus.DataSets;
using Garage_Hangar_Exercise5.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types
{
    public class Car : Vehicle
    {
        public Car(string licensePlate,
                   DateTime entryTime,
                   DateTime? exitTime,
                   int numberOfEngines,
                   double engineVolume,
                   string fuelType,
                   string brand,
                   string color)

            : base(licensePlate,
                   entryTime,
                   exitTime,
                   numberOfEngines,
                   engineVolume,
                   fuelType,
                   brand,
                   color)

        {
        }

        public override double CalculateBillingAmount(TimeSpan timeParked)
        {
            double hourlyRate = Vehicle.BillingRates["Car"];  // Default rate for cars

            if (FuelType.Equals("Electric", StringComparison.InvariantCultureIgnoreCase))
            {
                hourlyRate += Vehicle.BillingRates["ElectricChargeRate"];
            }

            return timeParked.TotalHours * hourlyRate;
        }




    }
}
