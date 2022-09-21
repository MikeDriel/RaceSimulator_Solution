using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
			X = 0;
			Y = 0;
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
						Direction = 270;
						PrintToConsole(_LeftCornerHorizontal);
						break;
					case SectionType.RightCorner:
						Direction = 180;
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
						Direction = 90;
						PrintToConsole(_LeftCornerVertical);
						break;
					case SectionType.RightCornerV:
						Direction = 0;
						PrintToConsole(_RightCornerVertical);
						break;
				}
			}
		}

		#region graphics
		private static string[] _finishHorizontal = { "-----", "  #  ", "     ", "  #  ", "-----" };
		private static string[] _StraightHorizontal = { "-----", "     ", "     ", "     ", "-----" };
		private static string[] _LeftCornerHorizontal = { "    |", "    |", "    |", "    |", "----/" };
		private static string[] _RightCornerHorizontal = { "----\\", "    |", "    |", "    |", "    |" };
		private static string[] _StartGridHorizontal = { "-----", " O   ", "     ", " O   ", "-----" };

		private static string[] _finishVertical = { "|   |", "|   |", "|# #|", "|   |", "|   |" };
		private static string[] _StraightVertical = { "|   |", "|   |", "|   |", "|   |", "|   |" };
		private static string[] _LeftCornerVertical = { "/----", "|    ", "|    ", "|    ", "|    " };
		private static string[] _RightCornerVertical = { "|    ", "|    ", "|    ", "|    ", "\\----" };
		private static string[] _StartGridVertical = { "|   |", "|   |", "|O O|", "|   |", "|   |" };
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
	}
}