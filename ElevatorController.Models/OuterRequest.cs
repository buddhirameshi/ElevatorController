

namespace ElevatorController.Models
{
    public class OuterRequest
    {
        public OuterRequest(int source, string direction)
        {
            Key = $"{source}-{direction}";
        }
        public string Key { get; }
        public int Source { get; set; }

        public Direction SelectedDirection { get; set; }

        public enum Direction { Up, Down }

    }
}
