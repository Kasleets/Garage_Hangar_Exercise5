
using Garage_Hangar_Exercise5.Garage_detailed;

namespace Garage_Hangar_Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Vehicle.BillingRate = configuration.GetValue<double>("BillingRate");


        }
    }
}