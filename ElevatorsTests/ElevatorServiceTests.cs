using Elevators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            _elevatorService = (ElevatorService)serviceProvider.GetService<IElevatorService>()!;
        }

        //for brevity
        //public ElevatorServiceTests(IElevatorService elevatorService)
        //{
        //    _elevatorService = (ElevatorService)elevatorService;
        //}

        [TestMethod]
        public void AddPassengerTest()
        {
            _elevatorService.AddPassenger(1, 2);
            int allPassengers = _elevatorService.Elevators.Sum(e => e.Passengers.Count);
            Assert.AreEqual(1, allPassengers);
        }

        [TestMethod()]
        public void MoveEmptyElevatorTest()
        {

            _elevatorService.MoveElevator();
            Assert.IsTrue(!_elevatorService.Elevators.Where(e => e.Status != ElevatorStatus.Idle).Any());
        }

        [TestMethod()]
        public void MoveSummonedElevatorTest()
        {
            _elevatorService.AddPassenger(1, 2);
            _elevatorService.MoveElevator();
            Assert.IsTrue(_elevatorService.Elevators.Where(e => e.Status != ElevatorStatus.Idle).Any());
        }

        //[TestMethod()]
        //public void FindClosestElevatorDistanceTest()
        //{
        //    Assert.Fail();
        //}
    }
}