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
        public string Color { get; set; }

        // Dictionary to store billing rates
        public static Dictionary<string, double> BillingRates { get; private set; } = new Dictionary<string, double>();

        public static void InitializeBillingRates(Dictionary<string, double> rates)
        {
            BillingRates = rates ?? throw new ArgumentNullException(nameof(rates));
        }

        // HashSet to ensure unique license plates
        private static HashSet<string> licensePlates = new HashSet<string>();

        protected Vehicle(string licensePlate,
                          DateTime entryTime,
                          DateTime? exitTime,
                          int numberOfEngines,
                          double engineVolume,
                          string fuelType,
                          string brand,
                          string color)
        {
            ValidateConstructorParameters(licensePlate); //, fuelType, brand);

            if (!licensePlates.Add(licensePlate))
            {
                throw new ArgumentException("License plate must be unique to a vehicle. Input VIN if not available.");
            }

            LicensePlate = licensePlate;
            EntryTime = entryTime;
            ExitTime = exitTime;
            NumberOfEngines = numberOfEngines;
            EngineVolume = engineVolume;
            FuelType = fuelType;
            Brand = brand;
            Color = color;
        }

        public double Cost
        {
            get
            {
                if (!ExitTime.HasValue)
                {
                    throw new ArgumentException($"This vehicle has not left the parking yet");
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

            double amount = timeParked.TotalHours * rate;
            return Math.Round(amount, 2);
        }

        // Logging method
        public virtual void LogBilling(TimeSpan timeParked)
        {
            var amount = CalculateBillingAmount(timeParked);
            Logger.LogAccounting($"Vehicle with license plate {LicensePlate} was billed {amount:F2}.");
        }

        private void ValidateConstructorParameters(string licensePlate) //(, string fuelType, string brand)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                throw new ArgumentException($"'{nameof(licensePlate)}' cannot be null or empty.", nameof(licensePlate));
            }

            //if (string.IsNullOrEmpty(fuelType))
            //{
            //    throw new ArgumentException($"'{nameof(fuelType)}' cannot be null or empty.", nameof(fuelType));
            //}

            //if (string.IsNullOrEmpty(brand))
            //{
            //    throw new ArgumentException($"'{nameof(brand)}' cannot be null or empty.", nameof(brand));
            //}
            
        }

        // Less memory efficient, but more future-proof that will display all of the unique properties of the derived classes
        // Shouldn't be overall a problem in this Parking Lot application.
        // Takes in consideration the String interpolation
        public override string ToString()
        {
            var properties = this.GetType().GetProperties()
                            .Where(p => p.Name != "Cost")  // Exclude the Cost property, to avoid system stack overflow when calling ToString() on the derived classes 
                            .Where(p => p.Name != "EntryTime"); // Exclude the EntryTime property, to avoid system stack overflow when calling ToString() on the derived classes
          
            var propertyValues = properties.Select(p =>
            {
                var value = p.GetValue(this);
                // Check for ExitTime and adjust the value if it's null
                if (p.Name == "ExitTime" && value == null)
                {
                    value = "Still Parked";
                }
                return $"\n{p.Name}: {value}";
            });

            return string.Join(" ", propertyValues);
        }




    }
}
