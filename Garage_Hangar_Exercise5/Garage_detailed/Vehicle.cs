using Garage_Hangar_Exercise5.Engine;
using Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Garage_detailed
{
    public abstract class Vehicle : Interfaces.IVehicle
    {
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int NumberOfEngines { get; set; }
        public double EngineVolume { get; set; }
        public string FuelType { get; set; }
        public string Brand { get; set; }
        public static double BillingRate { get; set; } = 30; // Default rate
        public static Dictionary<string, double> BillingRates { get; set; } = new Dictionary<string, double>();


        protected Vehicle(string licensePlate,
                          DateTime entryTime,
                          DateTime? exitTime,
                          int numberOfEngines,
                          double engineVolume,
                          string fuelType,
                          string brand)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                throw new ArgumentException($"'{nameof(licensePlate)}' cannot be null or empty.", nameof(licensePlate));
            }

            if (string.IsNullOrEmpty(fuelType))
            {
                throw new ArgumentException($"'{nameof(fuelType)}' cannot be null or empty.", nameof(fuelType));
            }

            if (string.IsNullOrEmpty(brand))
            {
                throw new ArgumentException($"'{nameof(brand)}' cannot be null or empty.", nameof(brand));
            }

            LicensePlate = licensePlate;
            EntryTime = entryTime;
            ExitTime = exitTime;
            NumberOfEngines = numberOfEngines;
            EngineVolume = engineVolume;
            FuelType = fuelType;
            Brand = brand;
        }

        public double Cost
        {
            get
            {
                if (!ExitTime.HasValue)
                {
                    throw new ArgumentException($"This vehicle has not left the parking yet"); // Not yet checked out
                }

                var timeParked = ExitTime.Value - EntryTime;
                return CalculateBillingAmount(timeParked);
            }
        }

        // This method is virtual, so that it can be overridden in derived classes
        public virtual double CalculateBillingAmount(TimeSpan timeParked)
        {
            double rate;
            if (BillingRates.ContainsKey(this.GetType().Name))
            {
                rate = BillingRates[this.GetType().Name];
            }
            else if (BillingRates.ContainsKey("Default"))
            {
                rate = BillingRates["Default"];
            }
            else
            {
                throw new InvalidOperationException("Default billing rate is not set.");
            }

            return timeParked.TotalHours * rate;
        }



        // Keeping the logging in the base class, since it's the same for all vehicles
        public virtual void LogBilling(TimeSpan timeParked)
        {
            var amount = CalculateBillingAmount(timeParked);
            Logger.LogAccounting($"Vehicle with license plate {LicensePlate} was billed {amount}.");
        }

    }
}