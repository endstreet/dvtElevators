using System.ComponentModel;

namespace Elevators
{
    public class Passenger
    {
        public Passenger(int fromLevel,int toLevel,DateTimeOffset? boarded = null)
        {
            Id = new Guid();
            FromLevel = fromLevel;
            ToLevel = toLevel;
            Boarded = boarded;
        }
        public Guid Id { get; set; }
        public int FromLevel { get; set; }
        public int ToLevel { get; set; }
        //Add waiting times to calculate performance
        public DateTimeOffset Summoned { get; set; } = DateTime.Now;
        //Add boarding times to calculate status (up/down/idle) from passenger list
        public DateTimeOffset? Boarded { get; set; }
    }
}