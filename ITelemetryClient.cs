namespace TelemetrySystemKata
{
    public interface ITelemetryClient
    {
        bool Connect(string telemetryServerConnectionString);

        bool Disconnect();

        void Send(string message);

        string Receive();
    }
}