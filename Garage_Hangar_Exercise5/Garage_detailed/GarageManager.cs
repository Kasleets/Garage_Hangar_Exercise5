using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    public class GarageManager
    {
        private List<Garage<Vehicle>> garages = new List<Garage<Vehicle>>();

        public GarageManager(int numberOfGarages, int capacityEach)
        {
            for (int i = 0; i < numberOfGarages; i++)
            {
                garages.Add(new Garage<Vehicle>(capacityEach));
            }
        }

        // ... methods to manage multiple garages
    }

}
