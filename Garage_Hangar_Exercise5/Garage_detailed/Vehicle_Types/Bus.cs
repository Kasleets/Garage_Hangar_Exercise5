using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types
{
    public class Bus : Vehicle
    {
        public int NumberOfSeats { get; set; }
        public Bus(string licensePlate,
                   DateTime entryTime,
                   DateTime? exitTime,
                   int numberOfEngines,
                   double engineVolume,
                   string fuelType,
                   string brand,
                   int numberOfSeats)

            : base(licensePlate,
                   entryTime,
                   exitTime,
                   numberOfEngines,
                   engineVolume,
                   fuelType,
                   brand) => NumberOfSeats = numberOfSeats;

    }
}
