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
		static int X;
		static int Y;
		static int Direction;
		static Race Race;

		public enum Directions
		{
			Up = 0,
			Right = 90,
			Down = 180,
			Left = 270
		}

		public static void Initialize(Race race)
		{
			X = 40;
			Y = 5;
			Direction = 90;
			Race = race;
		}

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
						determineDirection(SectionType.LeftCorner, Direction);
						PrintToConsole(_LeftCornerHorizontal, Race.GetSectionData(section));
						break;
					case SectionType.RightCorner:
						determineDirection(SectionType.RightCorner, Direction);
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
						determineDirection(SectionType.LeftCornerV, Direction); ;
						PrintToConsole(_LeftCornerVertical, Race.GetSectionData(section));
						break;
					case SectionType.RightCornerV:
						determineDirection(SectionType.RightCornerV, Direction);
						PrintToConsole(_RightCornerVertical, Race.GetSectionData(section));
						break;
				}
			}
		}

		//Hier staan de tracks
		#region graphics
		private static string[] _finishHorizontal = {
			"-----",
			"1 #  ",
			"  #  ",
			" 2#  ",
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
			" 1  O ",
			"     ",
			" 2  O ",
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
			"|  2 ",
			"|1   ",
			"|    ",
			"|   /" };
		private static string[] _RightCornerVertical = {
			"|   \\",
			"|  2 ",
			"|    ",
			"|  1 ",
			"\\----" };
		private static string[] _StartGridVertical = {
			"|   |",
			"|   |",
			"|O O|",
			"|1 2|",
			"|   |" };
		#endregion

		public static string ReplaceString(string stringMetNummer, IParticipant participant1, IParticipant participant2)
		{

			return stringMetNummer.Replace("1", participant1.Name[0].ToString()).Replace("2", participant2.Name[0].ToString());
		}

		public static void PrintToConsole(string[] tekenArray, SectionData sectionData)
		{
			foreach (string s in tekenArray)
			{
				string temp = s;
				
				if (sectionData.Left != null && sectionData.Right != null)
				{
					temp = ReplaceString(s, sectionData.Left, sectionData.Right);
				}
				

				Console.SetCursorPosition(X, Y);
				Console.Write(temp.Replace("1", " ").Replace("2", " "));

				Y++;
			}

			if (Direction == 90)
			{
				Y += -5;
				X += 5;
			}
			if (Direction == 270)
			{
				Y += -5;
				X += -5;
			}
			if (Direction == 0)
			{
				Y += -10;
			}

		}
		public static void determineDirection(SectionType sectiontype, int directionOfTrack)
		{
			switch (sectiontype)
			{
				case SectionType.RightCorner:
					if (directionOfTrack == 90)
					{
						Direction = 180;
					}
					else if (directionOfTrack == 0)
					{
						Direction = 270;
					}
					break;
				case SectionType.LeftCorner:
					if (directionOfTrack == 180)
					{
						Direction = 270;
					}
					else if (directionOfTrack == 90)
					{
						Direction = 0;
					}
					break;
				case SectionType.RightCornerV:
					if (directionOfTrack == 270)
					{
						Direction = 0;
					}
					else if (directionOfTrack == 180)
					{
						Direction = 90;
					}
					break;
				case SectionType.LeftCornerV:
					if (directionOfTrack == 0)
					{
						Direction = 90;
					}
					else if (directionOfTrack == 270)
					{
						Direction = 180;
					}
					break;
			}
		}
	}
}