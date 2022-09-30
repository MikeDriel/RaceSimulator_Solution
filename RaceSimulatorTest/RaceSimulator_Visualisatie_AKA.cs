using Controller;
using RaceSimulator_Project;

namespace RaceSimulatorTest
{

	[TestFixture]
	public class RaceSimulator_Visualisatie
	{
		[SetUp]
		public void Setup()
		{
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			Visualisatie.Initialize(Data.CurrentRace);

			Visualisatie.DrawTrack(Data.CurrentRace.Track);
		}

		[Test]
		public void DataGetsInitialized()
		{
			Assert.AreEqual(Visualisatie.Y);
		}
	}
}