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

		//[Test]
		//public void ()
		//{
		//	Race result = Data.CurrentRace;
		//	Assert.IsNotNull(result);
		//}

		//[Test]
		//public void CurrentRaceNotNull()
		//{
		//	Race result = Data.CurrentRace;
		//	Assert.IsNotNull(result);
		//}
	}
}
