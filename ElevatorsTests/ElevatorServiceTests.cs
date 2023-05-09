using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elevators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Elevators.Tests
{
    [TestClass()]
    public class ElevatorServiceTests
    {
        private readonly ElevatorService _elevatorService;
        public ElevatorServiceTests() 
        {
            var services = new ServiceCollection();
            services.AddSingleton<App>();
            services.AddScoped<IElevatorService, ElevatorService>();
            var serviceProvider = services.BuildServiceProvider();
            _elevatorService = serviceProvider.GetService<IElevatorService>() as ElevatorService;
        }

        [TestMethod]
        public void AddPassengerTest()
        {
            _elevatorService.AddPassenger(1, 2);
            int allPassengers = _elevatorService.Elevators.Sum(e => e.Passengers.Count);
            Assert.AreEqual(1, allPassengers);
        }
    }
}