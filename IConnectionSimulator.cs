namespace TDDMicroExercises.TelemetrySystem
{
    using System;

    public class ConnectionSimulator : IConnectionSimulator
    {
        private readonly Random _connectionEventsSimulator = new Random(42);

        public int Next(int minValue, int maxValue)
        {
            return _connectionEventsSimulator.Next(minValue, maxValue);
        }
    }

    public interface IConnectionSimulator
    {
        int Next(int minValue, int maxValue);
    }
}