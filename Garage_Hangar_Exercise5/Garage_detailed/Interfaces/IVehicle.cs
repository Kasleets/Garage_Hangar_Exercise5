using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed.Interfaces
{
    public interface IVehicle
    {
        string LicensePlate { get; set; }
        DateTime EntryTime { get; set; }
        DateTime? ExitTime { get; set; }
        int NumberOfEngines { get; set; }
        double EngineVolume { get; set; }
        string FuelType { get; set; }
        string Brand { get; set; }
        string Color { get; set; }

        //Todo: figure out how to enforce giving the cost to customer
        


    }
}
