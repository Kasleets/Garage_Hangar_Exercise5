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
        private T?[] vehicles; // Note: We're allowing nullable values here.

        // This HashSet will keep track of occupied slots
        private HashSet<int> occupiedSlots = new HashSet<int>();

        public Garage(int capacity)
        {
            vehicles = new T?[capacity];
        }

        public bool ParkVehicle(T vehicle)
        {
            // Find first available slot
            int? availableSlot = FindAvailableSlot();

            if (availableSlot.HasValue)
            {
                vehicles[availableSlot.Value] = vehicle;
                occupiedSlots.Add(availableSlot.Value);
                return true;
            }
            else
            {
                return false; // Garage is full
            }
        }

        public bool RemoveVehicle(string licensePlate)
        {
            int? vehicleIndex = FindVehicleIndex(licensePlate);

            if (vehicleIndex.HasValue)
            {
                vehicles[vehicleIndex.Value] = null;
                occupiedSlots.Remove(vehicleIndex.Value);
                return true;
            }
            else
            {
                return false; // Vehicle not found
            }
        }

        private int? FindAvailableSlot()
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (!occupiedSlots.Contains(i))
                {
                    return i;
                }
            }

            return null; // No available slots
        }

        private int? FindVehicleIndex(string licensePlate)
        {
            return vehicles.ToList().FindIndex(v => v?.LicensePlate == licensePlate);
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
                    yield return vehicles[i]!;
            }
        }

        public void ListAllParkedVehicles()
        {
            Console.WriteLine("List of All Parked Vehicles:");
            Console.WriteLine("----------------------------------");

            int count = 0;
            foreach (var vehicle in this)
            {
                count++;
                Console.WriteLine($"Vehicle {count}:");
                Console.WriteLine($"License Plate: {vehicle.LicensePlate}");
                Console.WriteLine($"Entry Time: {vehicle.EntryTime}");
                Console.WriteLine($"Number of Engines: {vehicle.NumberOfEngines}");
                Console.WriteLine($"Engine Volume: {vehicle.EngineVolume}");
                Console.WriteLine($"Fuel Type: {vehicle.FuelType}");
                Console.WriteLine($"Brand: {vehicle.Brand}");
                Console.WriteLine("----------------------------------");
            }

            if (count == 0)
            {
                Console.WriteLine("The garage is currently empty.");
            }
        }



    }
}