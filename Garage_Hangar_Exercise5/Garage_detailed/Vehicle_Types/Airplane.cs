using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types
{
    public class Airplane : Vehicle
    {
        public int WingSpan { get; set; }

        public Airplane(string licensePlate,
                        DateTime entryTime,
                        DateTime? exitTime,
                        int numberOfEngines,
                        double engineVolume,
                        string fuelType,
                        string brand,
                        int wingSpan)

               : base(licensePlate,
                      entryTime,
                      exitTime,
                      numberOfEngines,
                      engineVolume,
                      fuelType,
                      brand) => WingSpan = wingSpan;
    }
}
