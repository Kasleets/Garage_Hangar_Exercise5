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
                Console.WriteLine($"Vehicle {count}: {vehicle}");  // Using the overridden ToString method of the Vehicle class
                Console.WriteLine("----------------------------------");
            }

            if (count == 0)
            {
                Console.WriteLine("The garage is currently empty.");
            }
        }



        public IEnumerable<T> GetAllParkedVehicles()
        {
            return vehicles.Where(vehicle => vehicle is not null)!;
        }

        public T? GetVehicle(string licensePlate)
        {
            // Using LINQ to find the vehicle by its license plate
            return vehicles.FirstOrDefault(v => v?.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase) == true);
        }

        public bool IsFull
        {
            get
            {
                return occupiedSlots.Count == vehicles.Length;
            }
        }

        public int Capacity
        {
            get
            {
                return vehicles.Length;
            }
        }

        public int AvailableSlots
        {
            get
            {
                return vehicles.Length - occupiedSlots.Count;
            }
        }

        // Search functionality for the garage by type
        public IEnumerable<T> FindVehicles(Func<T, bool> predicate)
        {
            return vehicles.OfType<T>().Where(predicate).ToList();
        }




    }
}