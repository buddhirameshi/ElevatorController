

namespace ElevatorController.Logic
{
    public interface IRequestHandler
    {
        void InitializeElevator();

        int FindOptimalWay();

        void CollectRequests();

        bool ProcessOuterRequest(string request);

        bool ProcessInnerRequest(string request);

    }
}
