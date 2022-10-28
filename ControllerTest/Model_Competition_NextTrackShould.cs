using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest
{

	[TestFixture]
	public class Model_Competition_NextTrackShould
	{
		private Competition _competition;

		[SetUp]
		public void Setup()
		{
			_competition = new Competition();
		}

		[Test]
		public void NextTrack_EmptyQueue_ReturnNull()
		{
			Track result = _competition.NextTrack();
			Assert.IsNull(result);
		}

		[Test]
		public void NextTrack_OneInQueue_ReturnTrack()
		{
			_competition.Tracks.Enqueue(new Track("TestTrack", Data.TrackBuilder("Vierkant")));
			Track result = _competition.NextTrack();
			Assert.AreEqual("TestTrack", result.Name);
		}

		[Test]
		public void NextTrack_OneInQueue_ReturnTrackFromQueue()
		{
			Track result;

			_competition.Tracks.Enqueue(new Track("TestTrack", Data.TrackBuilder("Vierkant")));
			result = _competition.NextTrack();
			result = _competition.NextTrack();
			Assert.IsNull(result);
		}

		[Test]
		public void NextTrack_TwoInQueue_ReturnNextTrack()
		{
			Track result;

			_competition.Tracks.Enqueue(new Track("TestTrack", Data.TrackBuilder("Vierkant")));
			_competition.Tracks.Enqueue(new Track("TestTrack2", Data.TrackBuilder("Vierkant")));
			result = _competition.NextTrack();
			Assert.AreEqual("TestTrack", result.Name);
			result = _competition.NextTrack();
			Assert.AreEqual("TestTrack2", result.Name);
		}
	}
}