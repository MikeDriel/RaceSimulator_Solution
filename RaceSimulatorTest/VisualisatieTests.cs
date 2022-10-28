using Controller;
using Model;
using NUnit.Framework;
using RaceSimulator_Project;
using System;

namespace RaceSimulatorTest
{
	[TestFixture]
	public class VisualisatieTests
	{

		[SetUp]
		public void Setup()
		{
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			Visualisatie.Initialize(Data.CurrentRace);
		}


		[Test]
		public void Initialize_CurrentRace_AreEqual()
		{
			// Arrange
			Race result = Data.CurrentRace;


			// Assert
			Assert.AreEqual(Visualisatie.Race, result);
		}


		[Test]
		public void DetermineDirection_Direction_AreNotEqual()
		{
			// Arrange
			Visualisatie.Direction direction = Visualisatie.Direction.Up;

			// Act
			Visualisatie.DetermineDirection(SectionTypes.LeftCorner, direction);
			
			// Assert
			Assert.AreNotEqual(Visualisatie.direction, direction);
		}

		//[Test]
		//public void OnRaceEnd_CleanUp_RaceIsNull()
		//{
		//	// Arrange
		//	Race result = Data.CurrentRace;
		//	Data.CurrentRace.CleanUp();
			
		//	// Assert
		//	Assert.AreNotEqual(null, result);
		//}
	}
}
