namespace TelemetrySystemKata
{
    using System;

    public class TelemetryClient : ITelemetryClient
    {
        public const string DiagnosticMessage = "AT#UD";

        public const string SimulatedDiagnosticMessageResult = "LAST TX rate................ 100 MBPS\r\n" + "HIGHEST TX rate............. 100 MBPS\r\n" + "LAST RX rate................ 100 MBPS\r\n" + "HIGHEST RX rate............. 100 MBPS\r\n" + "BIT RATE.................... 100000000\r\n" + "WORD LEN.................... 16\r\n" + "WORD/FRAME.................. 511\r\n" + "BITS/FRAME.................. 8192\r\n" + "MODULATION TYPE............. PCM/FM\r\n" + "TX Digital Los.............. 0.75\r\n" + "RX Digital Los.............. 0.10\r\n" + "BEP Test.................... -5\r\n" + "Local Rtrn Count............ 00\r\n" + "Remote Rtrn Count........... 00";

        private readonly IConnectionSimulator _simulator;

        private string _diagnosticMessageResult = string.Empty;

        public TelemetryClient(IConnectionSimulator simulator)
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

        public void Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException();
            }

            if (message == DiagnosticMessage)
            {
                // simulate a status report
                _diagnosticMessageResult = SimulatedDiagnosticMessageResult;
            }

            // here should go the real Send operation
        }

        public string Receive()
        {
            string diagnosticMessageResult;

            if (!string.IsNullOrEmpty(_diagnosticMessageResult))
            {
                diagnosticMessageResult = _diagnosticMessageResult;
                _diagnosticMessageResult = string.Empty;
            }
            else
            {
                // simulate a received message
                diagnosticMessageResult = string.Empty;
                int messageLength = _simulator.Next(50, 110);
                for (int i = messageLength; i >= 0; --i)
                {
                    diagnosticMessageResult += (char)_simulator.Next(40, 126);
                }
            }

            return diagnosticMessageResult;
        }
    }
}