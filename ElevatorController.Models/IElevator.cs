

namespace ElevatorController.Models
{
    public interface IElevator
    {
        int CurrentFloor { get; set; }

        int MinFloor { get; }

        int MaxFloor { get; }

        Status CurrentStatus { get; set; }

        enum Status
        {
            idle,
            goingUp,
            goingDown
        }

        void Move();
        void OpenDoor();

        void CloseDoor();

        void GetOuterRequests();

        void GetInnerRequests();

        void IntroduceSelf();
    }
}
