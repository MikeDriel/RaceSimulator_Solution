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
		static int AantalSections;
		static int Teller;
		public static void Initialize()
		{
			X = 0;
			Y = 0;
			AantalSections = 0;
			Teller = 0;
		}

		public static void DrawTrack(Track track)
		{
			foreach (Section section in track.Sections)
			{
				switch (section.SectionTypes)
				{
					case SectionType.StartGrid:
						PrintToConsole(_StartGridHorizontal);
						break;
					case SectionType.Straight:
						PrintToConsole(_StraightHorizontal);
						break;
					case SectionType.Finish:
						PrintToConsole(_finishHorizontal);
						Teller--;
						break;
					case SectionType.LeftCorner:
						PrintToConsole(_LeftCornerHorizontal);
						break;
					case SectionType.RightCorner:
						PrintToConsole(_RightCornerHorizontal);
						break;
					case SectionType.StartGridV:
						PrintToConsole(_StartGridVertical);
						break;
					case SectionType.StraightV:
						PrintToConsole(_StraightVertical);
						break;
					case SectionType.FinishV:
						PrintToConsole(_finishVertical);
						Teller--;
						break;
					case SectionType.LeftCornerV:
						PrintToConsole(_LeftCornerVertical);
						break;
					case SectionType.RightCornerV:
						PrintToConsole(_RightCornerVertical);
						break;
				}
			}
		}

		#region graphics
		private static string[] _finishHorizontal = { "-----", "  #  ", "     ", "  #  ", "-----" };
		private static string[] _StraightHorizontal = { "-----", "     ", "     ", "     ", "-----" };
		private static string[] _LeftCornerHorizontal = { };
		private static string[] _RightCornerHorizontal = { "----\\", "    |", "    |", "    |", "    |" };
		private static string[] _StartGridHorizontal = { };

		private static string[] _finishVertical = { };
		private static string[] _StraightVertical = { "|   |", "|   |", "|   |", "|   |", "|   |" };
		private static string[] _LeftCornerVertical = { };
		private static string[] _RightCornerVertical = { };
		private static string[] _StartGridVertical = { };
		#endregion

		public static void PrintToConsole(string[] tekenArray)
		{
			if (Teller > 5)
			{
				X = X - 5;
				foreach (string s in tekenArray)
				{
					Console.SetCursorPosition(X, Y);
					Console.Write(s);

					Y++;
				}
				
			}
			else
			{
				foreach (string s in tekenArray)
				{
					Console.SetCursorPosition(X, Y);
					Console.Write(s);

					Y++;
				}
				Teller++;

				X = X + 5;
				Y = 0;
			}
		}
	}
}