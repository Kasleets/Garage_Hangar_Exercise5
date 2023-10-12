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

            var garage = new Garage<Vehicle>(10); // Assuming a capacity of 10 vehicles for this test
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

            var garage = new Garage<Vehicle>(10); // Assuming a capacity of 30 cars for this test
            var bus = new Bus("KOT123", DateTime.Now, null, 1, 3900, "Petrol", "Mazda", 24, "Black");


            // Act

            bool isParked = garage.ParkVehicle(bus);

            // Assert

            Assert.True(isParked);

            var retrievedBus = garage.GetVehicle(bus.LicensePlate);
            Assert.NotNull(retrievedBus);
            Assert.Equal(bus.LicensePlate, retrievedBus.LicensePlate);


        }

        [Theory]

        [InlineData("VehicleType.Car", "POP193", null, null)]
        [InlineData("VehicleType.Bus", "KOH183", 32, null)] // 32 seats for the bus
        [InlineData("VehicleType.Airplane", "FLY456", null, 70)] // WingSpan of 70 for the airplane

        public void Should_ParkVehicles_Successfully(string vehicleType, string licensePlate, int? numberOfSeats, int? wingSpan)
        {
            // Arrange
            var garage = new Garage<Vehicle>(10);

            Vehicle vehicle;

            switch (vehicleType)
            {
                case "VehicleType.Car":
                    vehicle = new Car(licensePlate, DateTime.Now, null, 1, 3900, "Petrol", "Brand", "Black");
                    break;
                case "VehicleType.Bus":
                    vehicle = new Bus(licensePlate, DateTime.Now, null, 1, 6900, "Diesel", "Brand", numberOfSeats!.Value, "Blue");
                    break;
                case "VehicleType.Airplane":
                    vehicle = new Airplane(licensePlate, DateTime.Now, null, 1, 12000, "JetFuel", "Brand", wingSpan!.Value, "White");
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }

            // Act
            bool isParked = garage.ParkVehicle(vehicle);

            // Assert
            Assert.True(isParked);

            var retrievedVehicle = garage.GetVehicle(vehicle.LicensePlate);
            Assert.NotNull(retrievedVehicle);
            Assert.Equal(vehicle.LicensePlate, retrievedVehicle.LicensePlate);
        }

        [Fact]
        public void Should_FailToParkVehicle_WhenGarageIsFull()
        {
            // Arrange
            var garage = new Garage<Vehicle>(1); // Creating a garage with capacity of only 1 vehicle
            var firstCar = new Car("CAR123", DateTime.Now, null, 1, 3900, "Petrol", "Brand", "Black");
            var secondCar = new Car("CAR456", DateTime.Now, null, 1, 3900, "Petrol", "Brand", "Red");

            // Act
            bool isFirstCarParked = garage.ParkVehicle(firstCar);
            bool isSecondCarParked = garage.ParkVehicle(secondCar); // This should fail since the garage is full

            // Assert

            // Flip Assert.True to Assert.False to see the test fail to confirm that the conditions are correct
            Assert.True(isFirstCarParked);
            Assert.False(isSecondCarParked); // Expecting this to be false since parking the second car should fail
        }



    }
}