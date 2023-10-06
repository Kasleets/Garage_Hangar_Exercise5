using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types
{
public class Truck : Vehicle
    {
        public double Length { get; set; }

           public Truck(string licensePlate,
                     DateTime entryTime,
                     DateTime? exitTime,
                     int numberOfEngines,
                     double engineVolume,
                     string fuelType,
                     string brand,
                     double length)

         : base(licensePlate,
                entryTime,
                exitTime,
                numberOfEngines,
                engineVolume,
                fuelType,
                brand)

        {
            Length = length;
        }


    }
}
