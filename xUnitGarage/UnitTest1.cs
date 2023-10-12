using Xunit;
using xUnitGarage;
using System;
using Garage_Hangar_Exercise5;
using Garage_Hangar_Exercise5.Garage_detailed.Vehicle_Types;
using Garage_Hangar_Exercise5.Garage_detailed;

namespace xUnitGarage
{
    public class UnitTest1
    {
        [Fact]
        public void Should_ParkVehicle_Car_Successfully()
        {
            // Arrange

            var garage = new Garage<Vehicle>(30); // Assuming a capacity of 30 cars for this test
            var car = new Car("UTI123", DateTime.Now, null, 1, 3900, "Petrol", "Mazda", "Black");


            // Act

            bool isParked = garage.ParkVehicle(car);

            // Assert

            Assert.True(isParked);

            var retrievedCar = garage.GetVehicle(car.LicensePlate);
            Assert.NotNull(retrievedCar);
            Assert.Equal(car.LicensePlate, retrievedCar.LicensePlate);


        }

        [Fact]
        public void Should_ParkVehicle_Bus_Successfully()
        {
            // Arrange

            var garage = new Garage<Vehicle>(30); // Assuming a capacity of 30 cars for this test
            var bus = new Bus("KOT123", DateTime.Now, null, 1, 3900, "Petrol", "Mazda", 24, "Black");


            // Act

            bool isParked = garage.ParkVehicle(bus);

            // Assert

            Assert.True(isParked);

            var retrievedBus = garage.GetVehicle(bus.LicensePlate);
            Assert.NotNull(retrievedBus);
            Assert.Equal(bus.LicensePlate, retrievedBus.LicensePlate);


        }
    }
}