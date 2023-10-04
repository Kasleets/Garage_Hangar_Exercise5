﻿using System;
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
    }

}
