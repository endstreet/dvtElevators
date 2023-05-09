
namespace Elevators
{
    public class Elevator
    {

        public Elevator()
        {
            //Start with an Empty elevator
            Passengers = new List<Passenger>();
        }
        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Current Elevator Level
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Calculate status from the pax queue for this elevator
        /// </summary>
        public ElevatorStatus Status
        {
            get
            {
                //empty car
                if (!Passengers.Any()) return ElevatorStatus.Idle;
                DateTimeOffset? onboard = Passengers.Where(p => p.Boarded != null).Min(p => p.Boarded);
                //boarded pax with destination level above elevel or non boarded pax waiting level above elevel
                if (Passengers.Where(p => p.Boarded == null && p.FromLevel > Level || p.Boarded == onboard && p.Boarded != null && p.ToLevel > Level).Any()) return ElevatorStatus.Up;
                //must be going down for other pax
                return ElevatorStatus.Down;
            }
        }
        public List<Passenger> Passengers;

        /// <summary>
        /// Calculate the distance to a new passenger level
        /// </summary>
        /// <param name="fromLevel">Boarding level</param>
        /// <returns>Distance in steps</returns>
        public int DistanceToLevel(int fromLevel)
        {
            int virtualLevel = 0;
            switch (Status)
            {
                case ElevatorStatus.Up:
                    if (fromLevel >= Level) return Math.Abs(fromLevel - Level);
                    virtualLevel = Passengers.Max(p => p.ToLevel);//Highest passenger level
                    return Math.Abs(fromLevel - virtualLevel);
                case ElevatorStatus.Down:
                    if (fromLevel <= Level) return Math.Abs(fromLevel - Level);
                    virtualLevel = Passengers.Min(p => p.ToLevel);//Lowest passenger level
                    return Math.Abs(fromLevel - virtualLevel);
                default:
                    return Math.Abs(fromLevel - Level);
            }
        }
    }
}