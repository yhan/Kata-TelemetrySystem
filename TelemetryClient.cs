namespace TelemetrySystemKata
{
    using System;

    public class TelemetryClient : ITelemetryClient
    {
        private readonly IConnector _connector;

        private readonly IChannel _channel;
        
        public TelemetryClient(IConnector connector, IChannel channel)
        {
            _connector = connector;
            _channel = channel;
        }

        public bool Connect(string telemetryServerConnectionString)
        {
            return _connector.Connect(telemetryServerConnectionString);
        }

        public bool Disconnect()
        {
            return _connector.Disconnect();
        }

        public void Send(string message)
        {
            _channel.Send(message);
        }

        public string Receive()
        {
            return _channel.Receive();
        }
    }
}