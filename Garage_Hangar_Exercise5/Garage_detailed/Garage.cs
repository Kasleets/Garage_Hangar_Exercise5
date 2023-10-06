using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    class Garage<T> where T : Vehicle
    {

        private T[] vehicles;

        public Garage(int capacity)
        {

            vehicles = new T[capacity];
        }

        //public bool ParkVehicle(T vehicle)
        //{
        //    // Implementation here...
        //}

        //public bool RemoveVehicle(string licensePlate)
        //{
        //    // Implementation here...
        //}

        // ... other methods
    }

}
