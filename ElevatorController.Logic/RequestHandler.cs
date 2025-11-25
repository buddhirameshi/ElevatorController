using ElevatorController.Logic;
using ElevatorController.Models;
using System.Transactions;

namespace ElevatorController.Logic
{

    public class RequestHandler : IRequestHandler
    {


        public IElevator thisElevator;
        public ElevatorRequest userReq;

        public RequestHandler(IElevator anyElevator)
        {
            thisElevator = anyElevator;
            userReq = new ElevatorRequest();
        }


        /// <summary>
        /// Initializes the elevator and starts collecting requests
        /// </summary>
        public void InitializeElevator()
        {
            thisElevator.IntroduceSelf();
            var userinput = Console.ReadLine();

            var isValidRequest = ProcessOuterRequest(userinput!);
            if (isValidRequest)
            {
                FindOptimalWay();
                CollectRequests();
            }
            else
            {
                while (!isValidRequest)
                {
                    userinput = Console.ReadLine();
                    isValidRequest = ProcessOuterRequest(userinput!);
                }
                FindOptimalWay();
                CollectRequests();

            }

        }

        /// <summary>
        /// Find the optimal way to serve the requests
        /// </summary>
        /// <returns></returns>
        public int FindOptimalWay()
        {
            var updatedRequests = userReq.AllRequests;
            var outerRequests = userReq.OuterRequests;
            int minGap = 200;
            int midFloor = (thisElevator.MinFloor + thisElevator.MaxFloor) / 2;
            if (updatedRequests != null && updatedRequests.Count > 0)
            {
                Console.WriteLine("The updated requests are as follows.");
                foreach (int i in updatedRequests)
                {
                    Console.WriteLine(i);

                }

            }

            int firstPriority = thisElevator.CurrentFloor;

            foreach (int oneRequest in updatedRequests!)
            {
                if (firstPriority == oneRequest)
                {
                    continue;
                }
                var tempGap = Math.Abs(thisElevator.CurrentFloor - oneRequest);

                //Find the closest floor to the current elevator position
                if (tempGap < minGap)
                {
                    minGap = tempGap;
                    firstPriority = oneRequest;
                }

                //If there are two floors with same gap
                else if (tempGap == minGap)
                {

                    //If the current floor is above the mid floor, prefer the higher floor
                    if (thisElevator.CurrentFloor > midFloor)
                    {
                        firstPriority = Math.Max(firstPriority, oneRequest);
                    }

                    //If the current floor is below the mid floor, prefer the lower floor
                    else if (thisElevator.CurrentFloor < midFloor)
                    {
                        firstPriority = Math.Min(firstPriority, oneRequest);
                    }

                    // when current floor is exactly the mid floor
                    else
                    {

                        //Separate the requests into two groups: smaller than current floor and greater than current floor
                        var smallerThan = updatedRequests.Where(x => x < thisElevator.CurrentFloor).ToList();
                        var greaterThan = updatedRequests.Where(x => x > thisElevator.CurrentFloor).ToList();

                        //Calculate the lowest floor gap between the smaller than and greater than groups.
                        //Ex: If the smaller than group has 3,4 and greater than group has 6,9. Then min gap is 9-3=6 and max gap is 6-4=2
                        var minValueGap = Math.Abs(greaterThan.Min() - smallerThan.Min());
                        var maxValueGap = Math.Abs(greaterThan.Max() - smallerThan.Max());

                        //If the min gap is greater than max gap, prefer the higher floor otherwise prefer the lower floor
                        //Higher the gap,higher the distance to be travelled to serve all requests
                        if (minValueGap > maxValueGap)
                        {
                            firstPriority = Math.Max(firstPriority, oneRequest);
                        }
                        else
                        {
                            firstPriority = Math.Min(firstPriority, oneRequest);
                        }
                    }
                }
            }
            thisElevator.CurrentStatus = firstPriority > thisElevator.CurrentFloor ? IElevator.Status.goingUp : (firstPriority < thisElevator.CurrentFloor ? IElevator.Status.goingDown : IElevator.Status.idle);
            thisElevator.CurrentFloor = firstPriority;
            updatedRequests.Remove(thisElevator.CurrentFloor);

            outerRequests.RemoveAll(r => r.Source == thisElevator.CurrentFloor);

            return firstPriority;

        }


        /// <summary>
        /// This method collects the requests until there is no pending request
        /// </summary>
        public void CollectRequests()
        {

            thisElevator.Move();
            thisElevator.OpenDoor();
            thisElevator.GetInnerRequests();
            var innerRequest = Console.ReadLine();
            thisElevator.CloseDoor();
            ProcessInnerRequest(innerRequest!);
            thisElevator.GetOuterRequests();
            var userinput = Console.ReadLine();
            Console.WriteLine("Thank you.");
            ProcessOuterRequest(userinput!);
            FindOptimalWay();

            while (userReq.AllRequests != null && userReq.AllRequests.Count > 0)
            {
                CollectRequests();
            }

            if (userReq.AllRequests == null || userReq.AllRequests.Count == 0)
            {
                thisElevator.Move();
                thisElevator.OpenDoor();
                thisElevator.GetInnerRequests();
                innerRequest = Console.ReadLine();
                var isValidRequest = ProcessInnerRequest(innerRequest!);
                if (isValidRequest)
                {
                    thisElevator.CloseDoor();
                    FindOptimalWay();
                    CollectRequests();
                }
                else
                {
                    thisElevator.CloseDoor();
                    while (!isValidRequest)
                    {
                        thisElevator.GetOuterRequests();
                        userinput = Console.ReadLine();
                        isValidRequest = ProcessOuterRequest(userinput!);
                    }
                    FindOptimalWay();
                    CollectRequests();

                }

            }

        }

        /// <summary>
        /// This method processes the requests coming from outside the elevator
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ProcessOuterRequest(string request)
        {
            bool isAdded = false;
            if (string.IsNullOrEmpty(request) && userReq.AllRequests == null)
            {
                thisElevator.CurrentStatus = IElevator.Status.idle;
                Console.WriteLine($"No requests. The lift is {thisElevator.CurrentStatus} at the floor number {thisElevator.CurrentFloor}");
                return isAdded;
            }


            else if (!string.IsNullOrEmpty(request))
            {
                string[] requests = request.Split(",");
                int requestCount = requests.Length;
                for (int i = 0; i < requests.Length; i++)
                {
                    string[] details = requests[i].Replace("[", "").Replace("]", "").Split("#");
                    if (details == null || details.Length < 2 || string.IsNullOrEmpty(details[0]) || string.IsNullOrEmpty(details[1]))
                    {
                        Console.WriteLine("Please enter a valid request in [3#d],[5#u],[8#u],[1#d] format");
                        return isAdded;
                    }

                    int requestedFrom = 0;
                    var direction = details[1].ToLower();
                    if (!int.TryParse(details[0], out requestedFrom) || requestedFrom < thisElevator.MinFloor || requestedFrom > thisElevator.MaxFloor || (!string.Equals(direction, "u") && !string.Equals(direction, "d")) || (requestedFrom == thisElevator.MinFloor && string.Equals(direction, "d")) || (requestedFrom == thisElevator.MaxFloor && string.Equals(direction, "u")))
                    {
                        Console.WriteLine("Please enter a valid request in [3#d],[5#u],[8#u],[1#d] format");
                        Console.WriteLine($"There are only {thisElevator.MaxFloor} floors. The highest floor is {thisElevator.MaxFloor} and the lowest floor is {thisElevator.MaxFloor}");
                        return isAdded;
                    }

                    if (userReq.OuterRequests == null)
                    {
                        userReq.OuterRequests = new List<OuterRequest>();
                    }

                    var key = $"{requestedFrom}-{direction}";
                    if (userReq.OuterRequests.Any(r => string.Equals(r.Key, key)))
                    {
                        continue;
                    }
                    OuterRequest oneRequest = new OuterRequest(requestedFrom, direction);
                    oneRequest.Source = requestedFrom;
                    oneRequest.SelectedDirection = string.Equals(direction, "u") ? OuterRequest.Direction.Up : OuterRequest.Direction.Down;
                    userReq.OuterRequests.Add(oneRequest);

                    if (userReq.AllRequests == null)
                    {
                        userReq.AllRequests = new List<int>();
                    }
                    if (!userReq.AllRequests.Any(x => x == requestedFrom))
                    {
                        userReq.AllRequests.Add(requestedFrom);
                        isAdded = true;
                    }
                }
            }
            if (userReq.AllRequests == null || userReq.AllRequests.Count == 0)
            {
                Console.WriteLine("Please enter a valid request in [3#d],[5#u],[8#u],[1#d] format");
                return isAdded;
            }
            return isAdded;


        }

        /// <summary>
        /// This method processes the requests coming from inside the elevator. Once a user is inside the elevator, he/she can request any floor.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ProcessInnerRequest(string request)
        {
            bool isAdded = false;
            if (string.IsNullOrEmpty(request))
            {
                return isAdded;
            }
            string[] requests = request.Split(",");
            int requestCount = requests.Length;
            for (int i = 0; i < requests.Length; i++)
            {

                int requestedFrom = 0;

                if (!int.TryParse(requests[i], out requestedFrom) || requestedFrom < thisElevator.MinFloor || requestedFrom > thisElevator.MaxFloor)
                {
                    Console.WriteLine("Please enter a valid request in 3,5,8,1 format");
                    Console.WriteLine($"There are only {thisElevator.MaxFloor} floors. The highest floor is {thisElevator.MaxFloor} and the lowest floor is {thisElevator.MaxFloor}");
                    return isAdded;
                }
                if (!userReq.AllRequests.Any(x => x == requestedFrom))
                {
                    userReq.AllRequests.Add(requestedFrom);
                    isAdded = true;
                }

            }
            if (userReq.AllRequests == null || userReq.AllRequests.Count == 0)
            {
                Console.WriteLine("Please enter a valid request in 3,5,8,1 format");
                return isAdded;
            }

            return isAdded;

        }


    }
}
