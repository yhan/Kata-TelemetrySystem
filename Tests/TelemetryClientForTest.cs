namespace TelemetrySystemKata.Tests
{
    using NSubstitute;

    public static class TelemetryClientForTest
    {
        public static ITelemetryClient SetUp(int nextReturnThis)
        {
            var simulator = Substitute.For<IConnectionSimulator>();
            simulator.Next(1, 10).Returns(nextReturnThis);
            var client = new TelemetryClient(new Connector(simulator), new Channel(simulator));

            return client;
        }
    }
}