using ElevatorController.Logic;
using ElevatorController.Models;
using FluentAssertions;

namespace ElevatorController.Tests
{

    public class ElevatorControllerLogicTests
    {
        private readonly IRequestHandler handler;
        private readonly IElevator elevator;

        public ElevatorControllerLogicTests()
        {
            int currentFloor = 0;
            int minFloor = 0;
            int maxFloor = 10;
            IElevator.Status currentStatus = IElevator.Status.idle;
            elevator = new Elevator(currentFloor, minFloor, maxFloor, currentStatus);
            handler = new RequestHandler(elevator);
        }



        [Fact]
        public void ProcessOuterRequest_Should_Return_True()
        {
            string outerRequests = "[8#d],[3#u],[10#d]";
            var result = handler.ProcessOuterRequest(outerRequests);
            result.Should().BeTrue();
        }



        [Fact]
        public void ProcessInnerRequest_Should_Return_True()
        {
            // Arrange
            string innerRequests = "2";

            // Act
            var result = handler.ProcessInnerRequest(innerRequests);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FindOptimalWay_Should_Return_ClosestFloor()
        {
            // Arrange - Add some requests first
            handler.ProcessInnerRequest("2,5,8");

            // Act
            var result = handler.FindOptimalWay();

            // Assert
            result.Should().Be(2); // Closest to starting floor 0
        }

        [Fact]
        public void Elevator_Should_Initialize_With_Correct_Values()
        {
            // Arrange & Act
            var testElevator = new Elevator(5, 0, 10, IElevator.Status.goingUp);

            // Assert
            testElevator.CurrentFloor.Should().Be(5);
            testElevator.MinFloor.Should().Be(0);
            testElevator.MaxFloor.Should().Be(10);
            testElevator.CurrentStatus.Should().Be(IElevator.Status.goingUp);
        }



    }
}
