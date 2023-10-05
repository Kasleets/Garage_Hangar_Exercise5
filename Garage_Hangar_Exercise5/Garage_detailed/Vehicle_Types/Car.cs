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
                   string brand)

            : base(licensePlate,
                   entryTime,
                   exitTime,
                   numberOfEngines,
                   engineVolume,
                   fuelType,
                   brand)

        {
        }

        public override double CalculateBillingAmount(TimeSpan timeParked)
        {
            double baseAmount = base.CalculateBillingAmount(timeParked);

            if (FuelType.Equals("Electric", StringComparison.InvariantCultureIgnoreCase))
            {
                double chargingAmount = timeParked.TotalHours * Vehicle.ElectricChargeRate;
                baseAmount += chargingAmount;
            }
            return baseAmount;
        }




    }
}
