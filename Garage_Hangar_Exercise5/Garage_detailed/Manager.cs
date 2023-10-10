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
            InitGarage();
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
            Console.WriteLine("1. Park Vehicle");
            Console.WriteLine("2. Search on reg number");
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
