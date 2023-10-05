using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Engine
{
    public static class Logger
    {
        private static string movementLogPath = $"{DateTime.Now:yyyyMMdd}-movements.log";
        private static string accountingLogPath = $"{DateTime.Now:yyyyMMdd}-accounting.log";

        public static void LogMovement(string message)
        {
            File.AppendAllText(movementLogPath, $"{DateTime.Now}: {message}\n");
        }

        public static void LogAccounting(string message)
        {
            File.AppendAllText(accountingLogPath, $"{DateTime.Now}: {message}\n");
        }
    }

}

/* *
 * Logger.LogMovement($"Vehicle with license plate {vehicle.LicensePlate} entered the garage at {vehicle.EntryTime}.");
 * Logger.LogMovement($"Vehicle with license plate {vehicle.LicensePlate} exited the garage at {vehicle.ExitTime}.");
 * Logger.LogAccounting($"Vehicle with license plate {vehicle.LicensePlate} was billed {amount}.");
 * 
 * */