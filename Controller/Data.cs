using Model;
using System.Collections.Generic;

namespace Controller
{
	public static class Data
	{
		public static Competition Competition;
		public static Race CurrentRace;
		public static Track currentTrack;


		public static void Initialize()
		{
		    Competition = new Competition();
			AddParticipants();
			AddTracks();
		}

		public static void AddParticipants()
		{
			Competition.Participants.Add(new Driver("Mike", 0, new Car(10,10,10,false), TeamColors.Red));
			Competition.Participants.Add(new Driver("Jan", 0, new Car(10, 10, 10, false), TeamColors.Green));
			Competition.Participants.Add(new Driver("Pieter", 0, new Car(10, 10, 10, false), TeamColors.Blue));
			Competition.Participants.Add(new Driver("Thomas", 0, new Car(10, 10, 10, false), TeamColors.Grey));
			Competition.Participants.Add(new Driver("Yasmine", 0, new Car(10, 10, 10, false), TeamColors.Yellow));
		}

		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("BloemendaalsTrack", new LinkedList<Section>()));
			Competition.Tracks.Enqueue(new Track("BigVroomVroomBrrr", new LinkedList<Section>()));
			Competition.Tracks.Enqueue(new Track("Zandvoort", new LinkedList<Section>()));
			Competition.Tracks.Enqueue(new Track("RainbowRoad", new LinkedList<Section>()));
			Competition.Tracks.Enqueue(new Track("KoopaTroopa", new LinkedList<Section>()));
		}
		
		public static void NextRace()
		{
			currentTrack = Competition.NextTrack();
			if (currentTrack != null)
			{
				CurrentRace = new Race(currentTrack, Competition.Participants);
			}
		}
	}
}