namespace Elevators
{
    public interface IElevatorService
    {
        List<Elevator> Elevators { get; set; }

        void AddPassenger(int FromLevel, int ToLevel);
    }
}