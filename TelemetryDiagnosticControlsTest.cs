namespace TDDMicroExercises.TelemetrySystem.Tests
{
    using System;

    using NFluent;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TelemetryDiagnosticControlsTest
    {
        [Test]
        public void CheckTransmission_should_send_a_diagnostic_message_and_receive_a_status_message_response()
        {
            var simulator = Substitute.For<IConnectionSimulator>();

            var client = new TelemetryClient(simulator);
            client.Send(TelemetryClient.DiagnosticMessage);

            var receivedMessage = client.Receive();

            Check.That(receivedMessage).IsEqualTo("LAST TX rate................ 100 MBPS\r\n" + "HIGHEST TX rate............. 100 MBPS\r\n" + "LAST RX rate................ 100 MBPS\r\n" + "HIGHEST RX rate............. 100 MBPS\r\n" + "BIT RATE.................... 100000000\r\n" + "WORD LEN.................... 16\r\n" + "WORD/FRAME.................. 511\r\n" + "BITS/FRAME.................. 8192\r\n" + "MODULATION TYPE............. PCM/FM\r\n" + "TX Digital Los.............. 0.75\r\n" + "RX Digital Los.............. 0.10\r\n" + "BEP Test.................... -5\r\n" + "Local Rtrn Count............ 00\r\n" + "Remote Rtrn Count........... 00");
        }

        [Test]
        public void CheckTransmission_should_send_a_not_expected_diagnostic_message_and_receive_a_status_message_response()
        {
            var simulator = Substitute.For<IConnectionSimulator>();
            simulator.Next(1, 10).Returns(1);

            var client = new TelemetryClient(simulator);
            client.Send("bla");

            var receivedMessage = client.Receive();

            Check.That(receivedMessage).IsEqualTo("\u0000");
        }

        [Test]
        public void OnlineStatus_returns_false_when_disconnect()
        {
            var simulator = Substitute.For<IConnectionSimulator>();

            var client = new TelemetryClient(simulator);
            client.Disconnect();
            Check.That(client.OnlineStatus).IsFalse();
        }

        [Test]
        public void Online_status_return_true_When_connect_is_done()
        {
            var simulator = Substitute.For<IConnectionSimulator>();
            simulator.Next(1, 10).Returns(1);

            var client = new TelemetryClient(simulator);
            client.Connect("fake connection string");

            Check.That(client.OnlineStatus).IsTrue();
        }

        [TestCase(null)]
        [TestCase("")]
        public void Throws_when_connect_given_that_connection_string_is_empty(string connectionString)
        {
            var simulator = Substitute.For<IConnectionSimulator>();
            simulator.Next(1, 10).Returns(1);

            var client = new TelemetryClient(simulator);
            Check.ThatCode(() => { client.Connect(connectionString); }).Throws<ArgumentNullException>();
        }

        [TestCase(null)]
        [TestCase("")]
        public void Throws_when_send_empty_message(string message)
        {
            var simulator = Substitute.For<IConnectionSimulator>();
            simulator.Next(1, 10).Returns(1);

            var client = new TelemetryClient(simulator);
            Check.ThatCode(() => { client.Send(message); }).Throws<ArgumentNullException>();
        }

    }
}