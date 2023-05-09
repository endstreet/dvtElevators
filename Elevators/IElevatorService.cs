namespace Elevators
{
    public interface IElevatorService
    {
        List<Elevator> Elevators { get; set; }
        Queue<Passenger> PaxWaiting { get; set; }

        void AddPassenger(int FromLevel, int ToLevel);
        void MoveElevator();
    }
}