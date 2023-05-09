namespace Elevators
{
    public class App
    {
        private readonly IElevatorService _elevatorService;

        private string? errorMessage;
        const int Levels = 10;
        public App(IElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        public void Run()
        {
            while (true)
            {
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = "Hint: FromFloor,ToFloor or <Enter> to Move Elevator";
                }
                //Update console
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.WriteLine($"Elevator\tFloor\tStatus\tPassengers (Valid floors: 0-{Levels}).");
                Console.WriteLine("--------\t-----\t------\t-----");
                foreach (Elevator e in _elevatorService.Elevators)
                {
                    Console.WriteLine($"{e.Id}\t\t{e.Level}\t{e.Status}\t{e.Passengers.Count}");
                }
                Console.WriteLine();
                Console.WriteLine(errorMessage);
                Console.Write("Add Passenger:");
                errorMessage = "";

                try
                {
                    var response = Console.ReadLine();
                    if (string.IsNullOrEmpty(response) || !response.Contains(','))
                    {
                        if (_elevatorService.PaxWaiting.Count > 0)
                        {
                            Passenger p = _elevatorService.PaxWaiting.Dequeue();
                            _elevatorService.AddPassenger(p.FromLevel, p.ToLevel);
                        }

                        _elevatorService.MoveElevator();
                        continue;
                    }
                    int fromFloor = Convert.ToInt32("0" + response.Trim().Split(",")[0]);
                    int toFloor = Convert.ToInt32("0" + response.Trim().Split(",")[1]);
                    if (toFloor > Levels || fromFloor == toFloor)
                    {
                        errorMessage = "Invalid To Floor";
                        continue;
                    }

                    _elevatorService.AddPassenger(fromFloor, toFloor);
                    _elevatorService.MoveElevator();
                }
                catch
                {
                    errorMessage = "Invalid Input";
                }
            }
        }
    }
}



