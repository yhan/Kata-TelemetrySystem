namespace TelemetrySystemKata
{
    using System;

    public class TelemetryDiagnosticControls
    {
        private const string DiagnosticChannelConnectionString = "*111#";

        private readonly ITelemetryClient _telemetryClient;

        public TelemetryDiagnosticControls(ITelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public string CheckTransmission()
        {
            var isOnline = _telemetryClient.Disconnect();

            int retryLeft = 3;
            while (!isOnline && retryLeft > 0)
            {
                isOnline = _telemetryClient.Connect(DiagnosticChannelConnectionString);
                retryLeft -= 1;
            }

            if (!isOnline)
            {
                throw new Exception("Unable to connect.");
            }

            _telemetryClient.Send(TelemetryClient.DiagnosticMessage);
            return _telemetryClient.Receive();
        }
    }
}