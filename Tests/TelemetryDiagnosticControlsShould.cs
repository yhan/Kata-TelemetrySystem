namespace TelemetrySystemKata.Tests
{
    using System;

    using NFluent;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TelemetryDiagnosticControlsShould
    {
        [Test]
        public void Receive_diagnostics_info_When_CheckTransmission()
        {
            var nextReturnThisMakesSuccessfulConnection = 1;
            var client = TelemetryClientForTest.SetUp(nextReturnThisMakesSuccessfulConnection);
            var telemetryDiagnosticControls = new TelemetryDiagnosticControls(client);

            var diagnosticsResult = telemetryDiagnosticControls.CheckTransmission();

            Check.That(diagnosticsResult).IsEqualTo(Channel.SimulatedDiagnosticMessageResult);
        }


        [Test]
        public void Receive_diagnostics_info_When_client_try_connect_twice_and_finished_by_connected_then_CheckTransmission()
        {
            var connectionSimulator = Simulate_client_try_connect_twice_and_finished_by_connected();

            var telemetryClient = new TelemetryClient(new Connector(connectionSimulator), new Channel(connectionSimulator));

            var telemetryDiagnosticControls = new TelemetryDiagnosticControls(telemetryClient);

            var diagnosticsResult = telemetryDiagnosticControls.CheckTransmission();

            Check.That(diagnosticsResult).IsEqualTo(Channel.SimulatedDiagnosticMessageResult);
        }

        private static IConnectionSimulator Simulate_client_try_connect_twice_and_finished_by_connected()
        {
            var connectionSimulator = Substitute.For<IConnectionSimulator>();
            connectionSimulator.Next(1, 10).Returns(100, 101, 8);
            return connectionSimulator;
        }

        [Test]
        public void CheckTransmission_fails_When_client_connection_fail()
        {
            var nextReturnThisMakesSuccessfulConnection = 100;
            var client = TelemetryClientForTest.SetUp(nextReturnThisMakesSuccessfulConnection);
            var telemetryDiagnosticControls = new TelemetryDiagnosticControls(client);

            Check.ThatCode(
                    () =>
                        {
                            telemetryDiagnosticControls.CheckTransmission();
                        })
                .Throws<Exception>()
                .WithMessage("Unable to connect.");
        }

        [Test]
        public void CheckTransmission_fails_When_client_connection_retry_times_are_all_used()
        {
            var connectionSimulator = SimulateClientRetryConnect_three_times_fail();

            var telemetryClient = new TelemetryClient(new Connector(connectionSimulator), new Channel(connectionSimulator));
            var telemetryDiagnosticControls = new TelemetryDiagnosticControls(telemetryClient);

            Check.ThatCode(
                    () =>
                        {
                            telemetryDiagnosticControls.CheckTransmission();
                        })
                .Throws<Exception>()
                .WithMessage("Unable to connect.");
        }

        private static IConnectionSimulator SimulateClientRetryConnect_three_times_fail()
        {
            var connectionSimulator = Substitute.For<IConnectionSimulator>();

            connectionSimulator.Next(1, 10).Returns(100, 101, 102);

            return connectionSimulator;
        }
    }
}