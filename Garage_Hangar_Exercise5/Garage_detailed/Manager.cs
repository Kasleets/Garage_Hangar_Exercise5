using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    public class Manager
    {
        private Garage<Vehicle> garage = default!;



       

        internal void Run()
        {
           // InitGarage();
            ShowMainMeny();

            //Get input from user
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ParkVehicle();
                    break;
                default:
                    break;
            }
        }

        private void ParkVehicle()
        {
            ShowParkVehicleMeny();

            //Get input from user
            //Switch
        }

        private void ShowParkVehicleMeny()
        {
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Buss");

        }

        private void ShowMainMeny()
        {
            Console.WriteLine("Welcome to Garage Manager, what would you like to do?");
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Remove Vehicle");
            Console.WriteLine("3. Search all cars by their license plate");
            Console.WriteLine("4. Look for a specific car by its license plate ");
            Console.WriteLine("5. See how many vehicles are parked inside a garage");
            Console.WriteLine("6. Look for a specific car based on its specifics");
            Console.WriteLine("0. Close the application");
        }

        private void InitGarage()
        {
            //Ask user for capacity
            int capacity = 10;
            garage = new Garage<Vehicle>(capacity);
            
        }

        // ... methods to manage multiple garages
    }

}
