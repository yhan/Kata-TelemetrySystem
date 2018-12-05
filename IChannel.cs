namespace TelemetrySystemKata
{
    public interface IChannel
    {
        void Send(string message);

        string Receive();
    }
}