
using static ElevatorController.Models.IElevator;

namespace ElevatorController.Models
{
    public class Elevator : IElevator
    {
        public Elevator(int currentFloor, int minFloor, int maxFloor, IElevator.Status status)
        {
            CurrentFloor = currentFloor;
            CurrentStatus = status;
            MinFloor = minFloor;
            MaxFloor = maxFloor;
        }
        public int CurrentFloor { get; set; }

        public int MinFloor { get; }

        public int MaxFloor { get; }

        public Status CurrentStatus { get; set; }



        public void Move()
        {
            Console.WriteLine("**********************");
            Console.WriteLine($"The lift stops at floor number {CurrentFloor}.");
        }

        public void OpenDoor()
        {
            Console.WriteLine("The door is open. Welcome aboard");
        }

        public void CloseDoor()
        {
            Console.WriteLine($"The door is closed. Let's Go");
        }

        public void GetOuterRequests()
        {
            Console.WriteLine("**********************");
            Console.WriteLine($"If you want to get in the lift enter the floor you are at right now and the direction that you wish to move. Ex: [3#u],[5#d]");
            Console.WriteLine("If there are no requests, press enter to proceed.");
        }

        public void GetInnerRequests()
        {
            Console.WriteLine($"Floor = {CurrentFloor}. Now that you are in, please enter the destination floors as a comma separated string. Ex: 3,8,7,8");
            Console.WriteLine("If there are no requests, press enter to proceed.");
            Console.WriteLine();
        }

        public void IntroduceSelf()
        {
            Console.WriteLine("Hello I'm the elevator controller");
            Console.WriteLine("**********************");
            Console.WriteLine();
            Console.WriteLine($"There are only {MaxFloor} floors. The highest floor is {MaxFloor} and the lowest floor is {MinFloor}");
            Console.WriteLine($"Assumption: Ground floor and the floor {MinFloor} refers to the same floor.");
            Console.WriteLine($"The lift is {CurrentStatus} and currently at the floor number {CurrentFloor}");
            Console.WriteLine($"If you want to get in the lift, enter the floor you are at right now and the direction that you wish to move. Refer the following example.");
            Console.WriteLine("There is a request from the 3rd floor to go up");
            Console.WriteLine("There is a request from the 5th floor to go down");
            Console.WriteLine("Then enter the command as a 2-D array in below format;");
            Console.WriteLine("[3#u],[5#d]");
            Console.WriteLine("Let's take one array element ex: [3#u]");
            Console.WriteLine("3 represents the requested floor. The letter u after the 3 separated by the comma is for the required directrion");
            Console.WriteLine("Use u for up and d for down");
            Console.WriteLine("All good? Let's go!. Enter as many requests as possible.");
        }

    }
}
