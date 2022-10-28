using Controller;
using Model;
using System.Data;

namespace RaceSimulatorTest
{
	[TestFixture]
	public class RaceTests
	{
		[Test]
		public void RandomizeEquipment_IsTrue()
		{
			// Arrange
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				participant.Equipment.Performance = 0;
				participant.Equipment.Quality = 0;
				participant.Equipment.Speed = 0;
			}
			bool randomized = true;

			// Act
			Data.CurrentRace.RandomizeEquipment();

			// Assert
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				if (participant.Equipment.Performance == 0 || participant.Equipment.Quality == 0 || participant.Equipment.Speed == 0)
				{
					randomized = false;
				}
			}

			Assert.That(randomized == true);
		}

		[Test]
		public void PlaceContestants_ContestantsOnTrack()
		{
			// Arrange
			Data.Initialize();
			Data.NextRace();


			// Act
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			// Assert
			if (Data.CurrentRace.Participants.Count > 0)
			{
				Assert.Pass();
			}
			else Assert.Fail();
		}
	}
}