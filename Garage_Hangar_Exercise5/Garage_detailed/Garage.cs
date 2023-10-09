using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    class Garage<T> : IEnumerable<T> where T : Vehicle
    {

        private T[] vehicles;

        public Garage(int capacity)
        {

            vehicles = new T[capacity];
        }

        // Implementation for the non-generic IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Implementation for the generic IEnumerable<T>
        // Todo: implement going through only the parked cars, not every each time through full capacity of the Garage
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null)
                    yield return vehicles[i];
            }
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
