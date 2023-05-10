namespace Elevators
{
    public class ElevatorService : IElevatorService
    {
        //Defaults
        const int MaxPassengers = 10;
        const int NoOfElevators = 3;
        //Black friday queue
        public Queue<Passenger> PaxWaiting { get; set; } = new Queue<Passenger>();
        public List<Elevator> Elevators { get; set; }

        public ElevatorService()
        {
            //initialize elevators
            Elevators = new List<Elevator>();
            for (int i = 0; i < NoOfElevators; i++)
            {
                Elevators.Add(new Elevator() { Id = i });
            }
        }

        public void AddPassenger(int FromLevel, int ToLevel)
        {
            //Get the relevant distances into a list
            List<int> distances = (Elevators.Select(e => e.DistanceToLevel(FromLevel))).ToList();
            //Get the available elevators (with space)
            var elevatorCandidates = Elevators.Where(e => e.DistanceToLevel(FromLevel) == distances.Min() && e.Passengers.Count <= MaxPassengers);
            if (elevatorCandidates.Any())
            {
                //Get the first elevator at min distance
                Elevator elevator = elevatorCandidates.First();
                //Add the passenger to the elevator
                elevator.Passengers.Add(new Passenger(FromLevel, ToLevel, FromLevel == elevator.Level ? DateTime.Now : null));

            }
            else
            {
                //Add the passenger to the Queue
                PaxWaiting.Enqueue(new Passenger(FromLevel, ToLevel));
            }
        }

        public void MoveElevator()
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

        /// <summary>
        /// Alternative Closest distance calculation
        /// Does not consider Capacity of Elevator
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <returns></returns>
        //public int FindClosestElevatorDistance(int FromLevel)
        //{
        //    return (Elevators.Select(e => e.DistanceToLevel(FromLevel))).ToList().OrderBy(x => Math.Abs(x - FromLevel)).First(); ;

        //}

    }
}
