using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types
{
    public class Boat : Vehicle
    {
        public int NumberOfFloors { get; set; }

        public Boat(string licensePlate,
                    DateTime entryTime,
                    DateTime? exitTime,
                    int numberOfEngines,
                    double engineVolume,
                    string fuelType,
                    string brand,
                    int numberOfFloors,
                    string color)

            : base(licensePlate,
                   entryTime,
                   exitTime,
                   numberOfEngines,
                   engineVolume,
                   fuelType,
                   brand,
                   color) => NumberOfFloors = numberOfFloors;
    }
}
