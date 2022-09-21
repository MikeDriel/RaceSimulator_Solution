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

		public enum Directions
		{
			Up = 0,
			Right = 90,
			Down = 180,
			Left = 270
		}

		public static void Initialize()
		{
			X = 40;
			Y = 5;
			Direction = 90;
		}

		public static void DrawTrack(Track track)
		{
			foreach (Section section in track.Sections)
			{
				switch (section.SectionTypes)
				{
					//Horizontals
					case SectionType.StartGrid:
						PrintToConsole(_StartGridHorizontal);
						break;
					case SectionType.Straight:
						PrintToConsole(_StraightHorizontal);
						break;
					case SectionType.Finish:
						PrintToConsole(_finishHorizontal);
						break;
					case SectionType.LeftCorner:
						determineDirection(SectionType.LeftCorner, Direction);
						PrintToConsole(_LeftCornerHorizontal);
						break;
					case SectionType.RightCorner:
						determineDirection(SectionType.RightCorner, Direction);
						PrintToConsole(_RightCornerHorizontal);
						break;

						//Verticals
					case SectionType.StartGridV:
						PrintToConsole(_StartGridVertical);
						break;
					case SectionType.StraightV:
						PrintToConsole(_StraightVertical);
						break;
					case SectionType.FinishV:
						PrintToConsole(_finishVertical);
						break;
					case SectionType.LeftCornerV:
						determineDirection(SectionType.LeftCornerV, Direction); ;
						PrintToConsole(_LeftCornerVertical);
						break;
					case SectionType.RightCornerV:
						determineDirection(SectionType.RightCornerV, Direction);
						PrintToConsole(_RightCornerVertical);
						break;
				}
			}
		}

		//Hier staan de tracks
		#region graphics
		private static string[] _finishHorizontal = { 
			"-----",
			"  #  ",
			"  #  ",
			"  #  ",
			"-----"
		};
		private static string[] _StraightHorizontal = { 
			"-----", 
			"     ", 
			"     ", 
			"     ", 
			"-----" };
		private static string[] _LeftCornerHorizontal = { 
			"/   |", 
			"    |", 
			"    |", 
			"    |", 

			"----/" };
		private static string[] _RightCornerHorizontal = { 
			"----\\", 
			"    |", 
			"    |", 
			"    |", 
			"\\   |" };
		private static string[] _StartGridHorizontal = { 
			"-----", 
			" O   ", 
			"     ", 
			" O   ", 
			"-----" };
		private static string[] _finishVertical = { 
			"|   |", 
			"|   |", 
			"|# #|", 
			"|   |", 
			"|   |" };
		private static string[] _StraightVertical = { 
			"|   |", 
			"|   |", 
			"|   |", 
			"|   |", 
			"|   |" };
		private static string[] _LeftCornerVertical = { 
			"/----", 
			"|    ", 
			"|    ", 
			"|    ", 
			"|   /" };
		private static string[] _RightCornerVertical = { 
			"|   \\", 
			"|    ", 
			"|    ", 
			"|    ", 
			"\\----" };
		private static string[] _StartGridVertical = { 
			"|   |", 
			"|   |", 
			"|O O|", 
			"|   |", 
			"|   |" };
		#endregion

		public static void PrintToConsole(string[] tekenArray)
		{
				foreach (string s in tekenArray)
				{
					Console.SetCursorPosition(X, Y);
					Console.Write(s);
				
					Y++;
				}

			if (Direction == 90 )
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
		public static void determineDirection(SectionType type, int dir)
		{
			switch (type)
			{
				case SectionType.RightCorner:
					if (dir == 90)
					{
						Direction = 180;
					}
					else if (dir == 0)
					{
						Direction = 270;
					}
					break;
				case SectionType.LeftCorner:
					if (dir == 180)
					{
						Direction = 270;
					}
					else if (dir == 90)
					{
						Direction = 0;
					}
					break;
				case SectionType.RightCornerV:
					if (dir == 270)
					{
						Direction = 0;
					}
					else if (dir == 180)
					{
						Direction = 90;
					}
					break;
				case SectionType.LeftCornerV:
					if (dir == 0)
					{
						Direction = 90;
					}
					else if (dir == 270)
					{
						Direction = 180;
					}
					break;
			}
		}
	}
}