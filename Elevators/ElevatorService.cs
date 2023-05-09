namespace Elevators
{
    public class ElevatorService : IElevatorService
    {
        //Defaults
        const int Levels = 10;
        const int MaxPassengers = 10;
        const int NoOfElevators = 3;
        //Black friday queue
        private Queue<Passenger> paxWaiting = new Queue<Passenger>();
        public List<Elevator> Elevators { get; set; }

        private string? errorMessage;
        public ElevatorService()
        {
            //initialize elevators
            Elevators = new List<Elevator>();
            for (int i = 0; i < NoOfElevators; i++)
            {
                Elevators.Add(new Elevator() { Id = i });
            }
            UpdateConsole();
        }

        public void AddPassenger(int FromLevel, int ToLevel)
        {
            //Get the relevant distances into a list
            List<int> distances = (Elevators.Select(e => e.DistanceToLevel(FromLevel))).ToList();
            //Get the available elevators
            var elevatorCandidates = Elevators.Where(e => e.DistanceToLevel(FromLevel) == distances.Min() && e.Passengers.Count <= MaxPassengers);
            if (elevatorCandidates.Any())
            {
                //Get the first elevator at min distance
                Elevator elevator = elevatorCandidates.First();
                //Add the passenger to the elevator
                elevator.Passengers.Add(new Passenger(FromLevel, ToLevel,FromLevel == elevator.Level ? DateTime.Now : null));
                
            }
            else
            {
                //Add the passenger to the Queue
                paxWaiting.Enqueue(new Passenger(FromLevel, ToLevel));
            }
        }

        private void MoveElevator()
        {
            //Move elevator
            foreach (var elevator in Elevators)
            {
                if (elevator.Status == ElevatorStatus.Up)
                {
                    elevator.Level++;
                }
                else if (elevator.Status == ElevatorStatus.Down)
                {
                    elevator.Level--;
                }
                //Off board passengers for this Level
                elevator.Passengers.RemoveAll(p => p.ToLevel == elevator.Level && p.Boarded != null);
                //On Board passengers for this Level
                foreach (var pax in elevator.Passengers.Where(p => p.FromLevel == elevator.Level && p.Boarded == null))
                {
                    pax.Boarded = DateTime.Now;
                }
            }
        }

        private void UpdateConsole()
        {
            while (true)
            {
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = "Hint<int>  FromFloor,ToFloor or <Enter> to Move Elevator";
                }
                //Update console
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.WriteLine("Elevator\tFloor\tStatus\tPassengers");
                Console.WriteLine("--------\t-----\t------\t-----");
                foreach (Elevator e in Elevators)
                {
                    Console.WriteLine($"{e.Id}\t\t{e.Level}\t{e.Status}\t{e.Passengers.Count}");
                }
                Console.WriteLine();
                Console.WriteLine("Add Passenger");
                Console.WriteLine(errorMessage);

                try
                {
                    var response = Console.ReadLine();
                    if (string.IsNullOrEmpty(response) || !response.Contains(','))
                    {
                        if (paxWaiting.Count > 0)
                        {
                            Passenger p = paxWaiting.Dequeue();
                            AddPassenger(p.FromLevel, p.ToLevel);
                        }

                        MoveElevator();
                        continue;
                    }
                    int fromFloor = Convert.ToInt32("0" + response.Trim().Split(",")[0]);
                    int toFloor = Convert.ToInt32("0" + response.Trim().Split(",")[1]);
                    if (toFloor > Levels || fromFloor == toFloor)
                    {
                        errorMessage = "Invalid To Floor";
                        continue;
                    }
                    
                    AddPassenger(fromFloor, toFloor);
                    MoveElevator();
                    errorMessage = "";
                }
                catch 
                {
                    errorMessage = "Invalid Input";
                }
            }

        }

    }
}
