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

        public App(IElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        public void Run()
        {

        }
    }
}



