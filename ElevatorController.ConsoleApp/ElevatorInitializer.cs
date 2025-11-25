using ElevatorController.Models;
using ElevatorController.Logic;

public static class ElevatorInitializer
{

    public static void Main()
    {
        int currentFloor = 1;
        int minFloor = 1;
        int maxFloor = 10;
        IElevator elevator = new Elevator(currentFloor, minFloor, maxFloor, IElevator.Status.idle);
        Console.WriteLine("Elevator Simulation Started.");
        IRequestHandler handler = new RequestHandler(elevator);
        handler.InitializeElevator();
        Console.ReadLine();
    }

}