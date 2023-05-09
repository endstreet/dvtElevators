namespace Elevators
{
    public interface IElevatorService
    {
        List<Elevator> Elevators { get; set; }
        Queue<Passenger> paxWaiting { get; set; }

        void AddPassenger(int FromLevel, int ToLevel);
        void MoveElevator();
    }
}