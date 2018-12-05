namespace TelemetrySystemKata
{
    public interface IConnector
    {
        bool Connect(string telemetryServerConnectionString);
        bool Disconnect();
    }
}