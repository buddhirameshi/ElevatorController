
namespace ElevatorController.Models
{
    public class ElevatorRequest
    {

        //There is no difference between inner and outer requests in terms of processing.But having this information will be useful when enhancemening the product.
        public List<OuterRequest> OuterRequests { get; set; } = new List<OuterRequest>();


        //All the requests(inner or outer) made to the elevator system will be stored here for processing.
        public List<int> AllRequests { get; set; } = new List<int>();




    }
}
