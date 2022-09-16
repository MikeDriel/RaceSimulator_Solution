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

		public static void Initialize()
		{
			
		}

		public static void DrawTrack(Track track)
		{
			foreach (Section section in track.Sections)
			{
				switch (section.SectionTypes)
				{
					case SectionType.StartGrid:
						Console.Write("S");
						break;
					case SectionType.Straight:
						Console.Write(_StraightHorizontal);
						break;
					case SectionType.Finish:
						Console.Write(_finishHorizontal);
						break;
					case SectionType.LeftCorner:
						Console.Write("/");
						break;
					case SectionType.RightCorner:
						Console.Write("\\");
						break;
				}
			}
		}

		#region graphics
		private static string[] _finishHorizontal = { "----", "  # ", "  # ", "----" };
		private static string[] _StraightHorizontal = { "----------" };
		private static string[] _LeftCornerHorizontal = { "----", "  # ", "  # ", "----" };
		private static string[] _RightCornerHorizontal = { "----", "  # ", "  # ", "----" };
		private static string[] _StartGridHorizontal = { "----", "  # ", "  # ", "----" };
		#endregion

		public static void print
	}

}
