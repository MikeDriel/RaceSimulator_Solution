using Model;
using System.Collections.Generic;

namespace Controller
{
	public static class Data
	{
		public static Competition Competition { get; set; }
		public static Race CurrentRace { get; set; }



		public static void Initialize()
		{
			Competition = new Competition();
			AddParticipants();
			AddTracks();
		}

		public static void AddParticipants()
		{
			Competition.Participants.Add(new Driver("Mike", 0, new Car(10, 10, 10, false), TeamColors.Red));
			Competition.Participants.Add(new Driver("Jan", 0, new Car(10, 10, 10, false), TeamColors.Green));
			Competition.Participants.Add(new Driver("Thomas", 0, new Car(10, 10, 10, false), TeamColors.Grey));
			Competition.Participants.Add(new Driver("Yasmine", 0, new Car(10, 10, 10, false), TeamColors.Yellow));
			//Competition.Participants.Add(new Driver("Pieter", 0, new Car(10, 10, 10, false), TeamColors.Blue));
			//Competition.Participants.Add(new Driver("KoopaTroopa", 0, new Car(10, 10, 10, false), TeamColors.Yellow));
		}

		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("Vierkant", TrackBuilder("Vierkant")));
			Competition.Tracks.Enqueue(new Track("Slang", TrackBuilder("Slang")));
			Competition.Tracks.Enqueue(new Track("Zandvoort", TrackBuilder("Vierkant")));
			Competition.Tracks.Enqueue(new Track("RainbowRoad", TrackBuilder("Vierkant")));
			Competition.Tracks.Enqueue(new Track("KoopaTroopa", TrackBuilder("Vierkant")));
		}

		public static void NextRace()
		{
			Track currentTrack = Competition.NextTrack();
			if (currentTrack != null)
			{
				CurrentRace = new Race(currentTrack, Competition.Participants);
			}
		}


		public static SectionType[] TrackBuilder(string trackName)
		{

			if (trackName.Equals("Vierkant"))
			{
				SectionType[] builder = new SectionType[]
				{
					SectionType.LeftCornerV,

					SectionType.Finish,
					SectionType.StartGrid,

					SectionType.RightCorner,

					SectionType.StraightV,
					SectionType.StraightV,

					SectionType.LeftCorner,

					SectionType.Straight,
					SectionType.Straight,

					SectionType.RightCornerV,

					SectionType.StraightV,
					SectionType.StraightV
				};
				return builder;
				}

				else if (trackName.Equals("Slang"))
				{
					SectionType[] builder = new SectionType[]
					{
					SectionType.Finish,
					SectionType.StartGrid,
					SectionType.RightCorner,
					SectionType.RightCornerV,
					SectionType.RightCorner,
					SectionType.RightCornerV,
					SectionType.RightCorner,
					SectionType.RightCornerV,
					SectionType.RightCorner,
					SectionType.RightCornerV,
					SectionType.RightCorner,
					SectionType.RightCornerV

					};
					return builder;
			}
			return null;
		}
	}
}