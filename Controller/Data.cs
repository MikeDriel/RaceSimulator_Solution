using Model;
using System.Collections.Generic;

namespace Controller
{
	public static class Data
	{
		public static Competition Competition { get; set; }
		public static Race CurrentRace { get; set; }


		//Initializes the competition, Participants and tracks
		public static void Initialize()
		{
			Competition = new Competition();
			AddParticipants();
			AddTracks();
		}

		//Adds participants to the competition
		public static void AddParticipants()
		{
			Competition.Participants.Add(new Driver("Mike", 0, new Car(), TeamColors.Blue));
			Competition.Participants.Add(new Driver("Jan", 0, new Car(), TeamColors.Green));
			Competition.Participants.Add(new Driver("Thomas", 0, new Car(), TeamColors.Yellow));
			Competition.Participants.Add(new Driver("Yasmine", 0, new Car(), TeamColors.Purple));
			//Competition.Participants.Add(new Driver("Pieter", 0, new Car(), TeamColors.Blue));
			//Competition.Participants.Add(new Driver("KoopaTroopa", 0, new Car(), TeamColors.Yellow));
		}

		//Adds tracks to the competition
		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("Vierkant", TrackBuilder("Vierkant")));
			Competition.Tracks.Enqueue(new Track("Slang", TrackBuilder("Slang")));
			Competition.Tracks.Enqueue(new Track("Zandvoort", TrackBuilder("Zandvoort")));
			Competition.Tracks.Enqueue(new Track("Loopa", TrackBuilder("Loopa")));
		}

		//Adds the track and participants to the current race
		public static Race NextRace()
		{
			Track currentTrack = Competition.NextTrack();
			if (currentTrack != null)
			{
				return CurrentRace = new Race(currentTrack, Competition.Participants);
			}
			return null;
		}

		//builder for the different races sorted by name
		public static SectionTypes[] TrackBuilder(string trackName)
		{

			if (trackName.Equals("Vierkant"))
			{
				SectionTypes[] builder = new SectionTypes[]
				{
					SectionTypes.LeftCornerV,

					SectionTypes.StartGrid,
					SectionTypes.Straight,
					SectionTypes.Finish,

					SectionTypes.RightCorner,

					SectionTypes.StraightV,
					SectionTypes.StraightV,

					SectionTypes.LeftCorner,

					SectionTypes.Straight,
					SectionTypes.Straight,
					SectionTypes.Straight,

					SectionTypes.RightCornerV,

					SectionTypes.StraightV,
					SectionTypes.StraightV
				};
				return builder;
			}

			else if (trackName.Equals("Slang"))
			{
				SectionTypes[] builder = new SectionTypes[]
				{
					SectionTypes.LeftCornerV,

					SectionTypes.StartGrid,
					SectionTypes.Straight,
					SectionTypes.Finish,
					SectionTypes.Straight,
					SectionTypes.Straight,
					SectionTypes.Straight,

					SectionTypes.RightCorner,

					SectionTypes.StraightV,
					SectionTypes.StraightV,

					SectionTypes.LeftCorner,

					SectionTypes.Straight,
					SectionTypes.Straight,
					SectionTypes.Straight,
					SectionTypes.Straight,
					SectionTypes.Straight,
					SectionTypes.Straight,

					SectionTypes.RightCornerV,

					SectionTypes.StraightV,
					SectionTypes.StraightV

				};
				return builder;
			}
			if (trackName.Equals("Zandvoort"))
			{
				SectionTypes[] builder = new SectionTypes[]
				{
					SectionTypes.LeftCornerV,

					SectionTypes.StartGrid,
					SectionTypes.Straight,
					SectionTypes.Finish,

					SectionTypes.RightCorner,

					SectionTypes.StraightV,
					SectionTypes.StraightV,
					SectionTypes.StraightV,
					SectionTypes.StraightV,


					SectionTypes.LeftCorner,

					SectionTypes.Straight,
					SectionTypes.Straight,
					SectionTypes.Straight,

					SectionTypes.RightCornerV,

					SectionTypes.StraightV,
					SectionTypes.StraightV,
					SectionTypes.StraightV,
					SectionTypes.StraightV,

				};
				return builder;
			}
			if (trackName.Equals("Loopa"))
			{
				SectionTypes[] builder = new SectionTypes[]
				{
					SectionTypes.LeftCornerV,

					SectionTypes.StartGrid,
					SectionTypes.Straight,
					SectionTypes.Finish,

					SectionTypes.RightCorner,

					SectionTypes.StraightV,
					SectionTypes.StraightV,

					SectionTypes.LeftCorner,

					SectionTypes.RightCornerV,
					SectionTypes.RightCorner,
					SectionTypes.Straight,
					SectionTypes.LeftCornerV,
					SectionTypes.LeftCorner,

					SectionTypes.RightCornerV,

					SectionTypes.StraightV,
					SectionTypes.StraightV
				};
				return builder;
			}
			return null;
		}
	}
}