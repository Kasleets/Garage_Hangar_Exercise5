using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Engine
{
    public static class Logger
    {
        private static string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static string movementLogPath = Path.Combine(logDirectory, $"{DateTime.Now:yyyyMMdd}-movements.log");
        private static string accountingLogPath = Path.Combine(logDirectory, $"{DateTime.Now:yyyyMMdd}-accounting.log");

        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }
        public static void LogMovement(string message)
        {
            EnsureDirectoryExists();
            File.AppendAllText(movementLogPath, $"{DateTime.Now}: {message}\n");
        }

        public static void LogAccounting(string message)
        {
            EnsureDirectoryExists();
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