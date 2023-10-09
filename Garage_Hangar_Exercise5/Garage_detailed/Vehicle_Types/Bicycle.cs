using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types
{
    public class Bicycle : Vehicle
    {
        public string Type { get; set; }
        public Bicycle(string licensePlate,
                       DateTime entryTime,
                       DateTime? exitTime,
                       int numberOfEngines,
                       double engineVolume,
                       string fuelType,
                       string brand,
                       string type)
            : base(licensePlate,
                   entryTime,
                   exitTime,
                   numberOfEngines,
                   engineVolume,
                   fuelType,
                   brand) => Type = type;
        
          
    public override double CalculateBillingAmount(TimeSpan timeParked)
        {
            double hourlyRate = Vehicle.BillingRates["Bicycle"];  

            if (FuelType.Equals("Electric", StringComparison.InvariantCultureIgnoreCase))
            {
                hourlyRate += Vehicle.BillingRates["ElectricChargeRate"];
            }

            return timeParked.TotalHours * hourlyRate;
        }

    
    }
}
