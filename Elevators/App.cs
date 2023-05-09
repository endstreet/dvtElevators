using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    errorMessage = "Hint<int>  FromFloor,ToFloor or <Enter> to Move Elevator";
                }
                //Update console
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.WriteLine("Elevator\tFloor\tStatus\tPassengers");
                Console.WriteLine("--------\t-----\t------\t-----");
                foreach (Elevator e in _elevatorService.Elevators)
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
                        if (_elevatorService.paxWaiting.Count > 0)
                        {
                            Passenger p = _elevatorService.paxWaiting.Dequeue();
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



