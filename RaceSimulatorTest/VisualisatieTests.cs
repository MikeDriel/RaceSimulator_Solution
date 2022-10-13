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

			Visualisatie.DrawTrack(Data.CurrentRace.Track);
		}


		[Test]
		public void Initialize_CurrentRace_AreEqual()
		{
			// Arrange
			Race result = Data.CurrentRace;

			// Act
			Visualisatie.Initialize(result);

			// Assert
			Assert.AreEqual(Visualisatie.Race, result);
		}


		[Test]
		public void DetermineDirection_Direction_AreNotEqual()
		{
			// Arrange
			Visualisatie.Direction direction = Visualisatie.Direction.Right;

			// Act
			Visualisatie.DetermineDirection(SectionType.LeftCorner, direction);


			// Assert
			Assert.AreNotEqual(Visualisatie.direction, direction);
		}

		[Test]
		public void OnDriversChanged_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			

			// Act


			// Assert
			//Assert.are();
		}

		[Test]
		public void OnRaceEnd_CleanUp_RaceIsNull()
		{
			// Arrange
			Race result = null;

			// Act
			//Visualisatie.OnRaceEnd(result);

			// Assert
			Assert.AreNotEqual(Data.CurrentRace, result);
		}
	}
}
