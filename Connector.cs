namespace TelemetrySystemKata
{
    using System;

    public class Connector : IConnector
    {
        private readonly IConnectionSimulator _simulator;

        public Connector(IConnectionSimulator simulator)
        {
            _simulator = simulator;
        }

        public bool Connect(string telemetryServerConnectionString)
        {
            if (string.IsNullOrEmpty(telemetryServerConnectionString))
            {
                throw new ArgumentNullException();
            }

            // simulate the operation on a real modem
            bool success = _simulator.Next(1, 10) <= 8;

            return success;
        }

        public bool Disconnect()
        {
            return false;
        }
    }
}