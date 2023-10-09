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

        public bool ParkVehicle(T vehicle)
        {
            // Check if the vehicle's license plate already exists in the garage
            if (vehicles.Any(v => v?.LicensePlate == vehicle.LicensePlate))
            {
                Console.WriteLine($"The vehicle with license plate {vehicle.LicensePlate} is already parked in the garage.");
                return false; // Indicate the vehicle wasn't parked
            }

            // 1. Check if the garage is full
            if (vehicles.All(v => v != null))
            {
                Console.WriteLine("The garage is full. Cannot park the vehicle.");
                return false; // Indicate the vehicle wasn't parked
            }

            // 2. Find the first empty spot and park the vehicle
            int emptySpotIndex = Array.IndexOf(vehicles, null);
            vehicles[emptySpotIndex] = vehicle;
            Console.WriteLine($"Vehicle with license plate {vehicle.LicensePlate} has been parked at spot {emptySpotIndex + 1}.");
            return true; // Indicate the vehicle was parked successfully
        }


        public bool RemoveVehicle(string licensePlate)
        {
            // 1. Find the vehicle by its license plate
            var vehicleToRemove = vehicles.FirstOrDefault(v => v?.LicensePlate == licensePlate);

            // Check if the vehicle was found
            if (vehicleToRemove == null)
            {
                Console.WriteLine($"No vehicle with license plate {licensePlate} was found in the garage.");
                return false; // Indicate the vehicle wasn't found
            }

            // 2. Remove the vehicle from the garage
            int vehicleIndex = Array.IndexOf(vehicles, vehicleToRemove);
            vehicles[vehicleIndex] = null;
            Console.WriteLine($"Vehicle with license plate {licensePlate} has been removed from the garage.");

            return true; // Indicate the vehicle was removed successfully
        }

    }

}
