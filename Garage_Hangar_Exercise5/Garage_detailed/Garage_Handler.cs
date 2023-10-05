using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    public class GarageHandler
    {
        private Garage<Vehicle> garage;
        public GarageHandler(int capacity)
        {
            garage = new Garage<Vehicle>(capacity);
        }

        // ... methods to interact with the garage
    }

}
