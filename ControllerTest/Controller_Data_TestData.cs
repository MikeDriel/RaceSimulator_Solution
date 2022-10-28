using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
	[TestFixture]
	public class Controller_Data_TestData
	{
		[SetUp]
		public void Setup()
		{
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);
		}
		
		[Test]
		public void CurrentRaceNotNull()
		{
			Race result = Data.CurrentRace;
			Assert.IsNotNull(result);
		}

		[Test]
		public void AddParticipantsAmoun_AreEqual()
		{
			Assert.AreEqual(Data.Competition.Participants.Count, 4);
		}

		[Test]
		public void CheckWinnerWhoWins_AreEqual()
		{
			IParticipant participant = Data.Competition.Participants[0];
			participant.Points = 12;
			Data.Competition.EndCompetition();
			Assert.AreEqual(Data.Competition.Winner, participant);
		}
	}
}
