using ElevatorController.Models;
using ElevatorController.Logic;

public static class ElevatorInitializer
{

    public static void Main()
    {
        int currentFloor = 1;
        const int MinFloor = 1;
        const int MaxFloor = 10;
        IElevator elevator = new Elevator(currentFloor, MinFloor, MaxFloor, IElevator.Status.idle);
        Console.WriteLine("Elevator Simulation Started.");
        IRequestHandler handler = new RequestHandler(elevator);
        handler.InitializeElevator();
        Console.ReadLine();
    }

}