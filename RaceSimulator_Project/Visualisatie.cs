using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static RaceSimulator_Project.Visualisatie;


namespace RaceSimulator_Project
{
	public static class Visualisatie
	{
		public static int x { get; set; }
		public static int y { get; set; }
		public static Direction direction { get; set; }
		public static Race Race { get; set; }


		public enum Direction
		{
			Up = 0,
			Right = 90,
			Down = 180,
			Left = 270
		}

		/// <summary>
		/// hey hallo
		/// 
		/// </summary>
		/// <param name="race"></param>
		public static void Initialize(Race race)
		{
			x = 40;
			y = 5;
			direction = Direction.Right;
			Race = race;

			Data.CurrentRace.DriversChanged += OnDriversChanged;
		}

		//Calls certain functions depending on the SectionType of the section
		public static void DrawTrack(Track track)
		{
			foreach (Section section in track.Sections)
			{
				switch (section.SectionTypes)
				{
					//Horizontals
					case SectionType.StartGrid:
						PrintToConsole(_StartGridHorizontal, Race.GetSectionData(section));
						break;
					case SectionType.Straight:
						PrintToConsole(_StraightHorizontal, Race.GetSectionData(section));
						break;
					case SectionType.Finish:
						PrintToConsole(_finishHorizontal, Race.GetSectionData(section));
						break;
					case SectionType.LeftCorner:
						DetermineDirection(SectionType.LeftCorner, direction);
						PrintToConsole(_LeftCornerHorizontal, Race.GetSectionData(section));
						break;
					case SectionType.RightCorner:
						DetermineDirection(SectionType.RightCorner, direction);
						PrintToConsole(_RightCornerHorizontal, Race.GetSectionData(section));
						break;

					//Verticals
					case SectionType.StartGridV:
						PrintToConsole(_StartGridVertical, Race.GetSectionData(section));
						break;
					case SectionType.StraightV:
						PrintToConsole(_StraightVertical, Race.GetSectionData(section));
						break;
					case SectionType.FinishV:
						PrintToConsole(_finishVertical, Race.GetSectionData(section));
						break;
					case SectionType.LeftCornerV:
						DetermineDirection(SectionType.LeftCornerV, direction); 
						PrintToConsole(_LeftCornerVertical, Race.GetSectionData(section));
						break;
					case SectionType.RightCornerV:
						DetermineDirection(SectionType.RightCornerV, direction);
						PrintToConsole(_RightCornerVertical, Race.GetSectionData(section));
						break;
				}
			}
		}

		//THE TRACKS ARE HERE!!!
		#region graphics
		private static string[] _finishHorizontal = {
			"-----",
			"1 #  ",
			"  #  ",
			"2 #  ",
			"-----"
		};
		private static string[] _StraightHorizontal = {
			"-----",
			" 1   ",
			"     ",
			"  2  ",
			"-----" };
		private static string[] _LeftCornerHorizontal = {
			"/   |",
			"  1 |",
			" 2  |",
			"    |",

			"----/" };
		private static string[] _RightCornerHorizontal = {
			"----\\",
			" 1  |",
			"    |",
			"  2 |",
			"\\   |" };
		private static string[] _StartGridHorizontal = {
			"-----",
			" 1  )",
			"    )",
			" 2  )",
			"-----" };
		private static string[] _finishVertical = {
			"|   |",
			"|1 2|",
			"|# #|",
			"|   |",
			"|   |" };
		private static string[] _StraightVertical = {
			"|   |",
			"|1  |",
			"|   |",
			"|  2|",
			"|   |" };
		private static string[] _LeftCornerVertical = {
			"/----",
			"|  1 ",
			"|    ",
			"| 2  ",
			"|   /" };
		private static string[] _RightCornerVertical = {
			"|   \\",
			"|  2 ",
			"|    ",
			"|  1 ",
			"\\----" };
		private static string[] _StartGridVertical = {
			"|   |",
			"|O O|",
			"|   |",
			"|1 2|",
			"|   |" };
		#endregion

		//Replaces the numbers in the track with the first letter of the drivers name
		private static string ReplaceString(string stringMetNummer, IParticipant participant1, IParticipant participant2)
		{
			
			return stringMetNummer.Replace("1", participant1.Name[0].ToString()).Replace("2", participant2.Name[0].ToString());
		}

		private static string ReplaceString(string stringMetNummer, IParticipant participant)
		{
			if (Race.GetSectionData(participant.CurrentSection).Left == participant)
			{
				return stringMetNummer.Replace("1", participant.Name[0].ToString());
			}
			else if (Race.GetSectionData(participant.CurrentSection).Right == participant)
			{
				return stringMetNummer.Replace("2", participant.Name[0].ToString());
			}
			return null;
		}
		

		//Print the track to the console
		public static void PrintToConsole(string[] tekenArray, SectionData sectionData)
		{
			foreach (string s in tekenArray)
			{
				string temp = s;

				if (sectionData.Left != null && sectionData.Right != null)
				{
					temp = ReplaceString(s, sectionData.Left, sectionData.Right);
				}
				else if (sectionData.Left != null)
				{
					temp = ReplaceString(s, sectionData.Left);
				}
				else if (sectionData.Right != null )
				{
					temp = ReplaceString(s, sectionData.Right);
				}


				Console.SetCursorPosition(x, y);
				Console.Write(temp.Replace("1", " ").Replace("2", " "));

				y++;
			}

			if (direction == Direction.Right)
			{
				y += -5;
				x += 5;
			}
			if (direction == Direction.Left)
			{
				y += -5;
				x += -5;
			}
			if (direction == Direction.Up)
			{
				y += -10;
			}
		}

		//Determine the direction of the track
		public static void DetermineDirection(SectionType sectiontype, Direction directionOfTrack)
		{
			switch (sectiontype)
			{
				case SectionType.RightCorner:
					if (directionOfTrack == Direction.Right)
					{
						direction = Direction.Down;
					}
					else if (directionOfTrack == Direction.Up)
					{
						direction = Direction.Left;
					}
					break;
				case SectionType.LeftCorner:
					if (directionOfTrack == Direction.Right)
					{
						direction = Direction.Up;
					}
					else if (directionOfTrack == Direction.Down)
					{
						direction = Direction.Left;
					}
					break;
				case SectionType.RightCornerV:
					if (directionOfTrack == Direction.Left)
					{
						direction = Direction.Up;
					}
					else if (directionOfTrack == Direction.Down)
					{
						direction = Direction.Right;
					}
					break;
				case SectionType.LeftCornerV: 
					if (directionOfTrack == Direction.Up)
					{
						direction = Direction.Right;
					}
					else if (directionOfTrack == Direction.Left)
					{
						direction = Direction.Down;
					}
					break;
			}
		}
		
		public static void OnDriversChanged(object source, DriversChangedEventArgs e)
		{
			Console.Clear();
			DrawTrack(e.Track);
		}
	}
}