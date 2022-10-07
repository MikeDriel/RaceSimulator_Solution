using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace WPFApp
{
	public static class WPFVisualize
	{
		public static int Xposition { get; set; }
		public static int Yposition { get; set; }
		public static int imageSize { get; set; }
		public static Direction direction { get; set; }
		public static Race Race { get; set; }
		public static int XimageScale { get; set; }
		public static int YimageScale { get; set; }
		public static int XtrackScale { get; set; }
		public static int YtrackScale { get; set; }

		public enum Direction
		{
			Up = 0,
			Right = 90,
			Down = 180,
			Left = 270
		}

		public static void Initialize(Race race)
		{
			Xposition = 1;
			Yposition = 1;

			direction = Direction.Right;
			Race = race;

			//Image properties
			imageSize = 200;
			XimageScale = 5;
			YimageScale = 5;
			
			//Track grote
			XtrackScale = 10;
			YtrackScale = 10;


			Data.CurrentRace.DriversChanged += OnDriversChanged;
			Data.CurrentRace.RaceEnd += OnRaceEnd;
		}

		//Calls certain functions depending on the SectionType of the section
		public static BitmapSource DrawTrack(Track track)
		{

			Bitmap bitmap = new Bitmap(XtrackScale * imageSize, YtrackScale * imageSize);
			Graphics graphics = Graphics.FromImage(bitmap);

			foreach (Section section in track.Sections)
			{
				MoveImagePointer();
				switch (section.SectionTypes)
				{
					//Horizontals
					case SectionType.StartGrid:
						graphics.DrawImage(PictureController.CloneImageFromCache(_StartGridHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.Straight:
						graphics.DrawImage(PictureController.CloneImageFromCache(_StraightHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.Finish:
						graphics.DrawImage(PictureController.CloneImageFromCache(_finishHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.LeftCorner:
						DetermineDirection(SectionType.LeftCorner, direction);
						graphics.DrawImage(PictureController.CloneImageFromCache(_LeftCornerHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.RightCorner:
						DetermineDirection(SectionType.RightCorner, direction);
						graphics.DrawImage(PictureController.CloneImageFromCache(_RightCornerHorizontal), ImageCalculationX(), ImageCalculationY());
						break;

					//Verticals
					case SectionType.StartGridV:
						graphics.DrawImage(PictureController.CloneImageFromCache(_StartGridVertical), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.StraightV:
						graphics.DrawImage(PictureController.CloneImageFromCache(_StraightVertical),  ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.FinishV:
						graphics.DrawImage(PictureController.CloneImageFromCache(_finishVertical), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.LeftCornerV:
						DetermineDirection(SectionType.LeftCornerV, direction);
						graphics.DrawImage(PictureController.CloneImageFromCache(_LeftCornerVertical), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.RightCornerV:
						DetermineDirection(SectionType.RightCornerV, direction);
						graphics.DrawImage(PictureController.CloneImageFromCache(_RightCornerVertical), ImageCalculationX(), ImageCalculationY());

						break;
				}
			}
			return (PictureController.CreateBitmapSourceFromGdiBitmap(bitmap));
		}


		//THE TRACKS ARE HERE!!!
		#region graphics
		//Horizontal
		private static string _StartGridHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StraightH.png";
		private static string _StraightHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StraightH.png";
		private static string _finishHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StraightH.png";
		private static string _LeftCornerHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerNW.png";
		private static string _RightCornerHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerSW.png";

		//Vertical
		private static string _StartGridVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerNE.png";
		private static string _StraightVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StraightV.png";
		private static string _finishVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerNE.png";
		private static string _LeftCornerVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerSE.png";
		private static string _RightCornerVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerNE.png";
		#endregion


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

		public static void MoveImagePointer()
		{
			switch (direction)
			{
				case Direction.Right:
					Xposition += 1;
					break;
				case Direction.Left:
					Xposition -= 1;
					break;
				case Direction.Up:
					Yposition -= 1;
					break;
				case Direction.Down:
					Yposition += 1;
					break;
			}
		}

		public static int ImageCalculationX()
		{
			return Xposition * imageSize;
		}

		public static int ImageCalculationY()
		{
			return Yposition * imageSize;
		}

		public static void OnDriversChanged(object source, DriversChangedEventArgs e)
		{
			//Console.Clear();
			//DrawTrack(e.Track);
		}

		public static void OnRaceEnd(object source, RaceEndEventArgs e)
		{
			//Initialize(Data.CurrentRace);
			//DrawTrack(Data.CurrentRace.Track);
		}
	}
}