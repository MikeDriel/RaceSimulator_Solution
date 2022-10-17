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
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Track = Model.Track;

namespace WPFApp
{
	public static class WPFVisualize
	{
		public static int Xposition { get; set; }
		public static int Yposition { get; set; }
		public static Direction direction { get; set; }
		public static Race Race { get; set; }

		//Track Width and Height
		public static int TrackWidth { get; set; }
		public static int TrackHeight { get; set; }
		public static int imageSize { get; set; }
		public static Graphics Graphics { get; set; }

		public enum Direction
		{
			Up = 0,
			Right = 90,
			Down = 180,
			Left = 270
		}

		public static void Initialize(Race race)
		{
			Xposition = 0;
			Yposition = 0;

			direction = Direction.Right;
			Race = race;

			//Image properties
			imageSize = 200;
			
			CalculateTrackSize();

			TrackWidth *= imageSize;
			TrackHeight *= imageSize;
		}

		//Calls certain functions depending on the SectionType of the section
		public static BitmapSource DrawTrack(Track track)
		{
			Bitmap bitmap = new Bitmap(TrackWidth, TrackHeight);
			Graphics = Graphics.FromImage(bitmap);

			foreach (Section section in track.Sections)
			{
				
				switch (section.SectionTypes)
				{
					//Horizontals
					case SectionType.StartGrid:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_StartGridHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.Straight:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_StraightHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.Finish:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_finishHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.LeftCorner:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_LeftCornerHorizontal), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.RightCorner:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_RightCornerHorizontal), ImageCalculationX(), ImageCalculationY());
						break;

					//Verticals
					case SectionType.StartGridV:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_StartGridVertical), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.StraightV:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_StraightVertical), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.FinishV:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_finishVertical), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.LeftCornerV:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_LeftCornerVertical), ImageCalculationX(), ImageCalculationY());
						break;
					case SectionType.RightCornerV:
						Graphics.DrawImage(PictureController.CloneImageFromCache(_RightCornerVertical), ImageCalculationX(), ImageCalculationY());
						break;
				}
				PlaceAllPartcipants(Graphics, Race, section);
				DetermineDirection(section.SectionTypes, direction);
				MoveImagePointer();
			}
			return (PictureController.CreateBitmapSourceFromGdiBitmap(bitmap));
		}


		//THE TRACKS ARE HERE!!!
		#region graphics
		//Horizontal
		private const string _StartGridHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StartH.png";
		private const string _StraightHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StraightH.png";
		private const string _finishHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\FinishH.png";
		private const string _LeftCornerHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerNW.png";
		private const string _RightCornerHorizontal = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerSW.png";

		//Vertical
		private const string _StartGridVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StartV.png";
		private const string _StraightVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\StraightV.png";
		private const string _finishVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\FinishV.png";
		private const string _LeftCornerVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerSE.png";
		private const string _RightCornerVertical = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\CornerNE.png";

		//Drivers
		private const string Player1 = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player1.png";
		private const string Player2 = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player2.png";
		private const string Player3 = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player3.png";
		private const string Player4 = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player4.png";

		//BrokenDrivers
		private const string Player1Broken = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player1Broken.png";
		private const string Player2Broken = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player2Broken.png";
		private const string Player3Broken = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player3Broken.png";
		private const string Player4Broken = "C:\\Users\\Calcium\\OneDrive - Windesheim Office365\\Jaar 2\\Semester 1\\Programmeren in C#\\PROJECT C#\\RaceSimulator_Solution\\WPFVisualize\\Graphics\\Player4Broken.png";
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

		//place picture of a participant on the WPF window on top of the track
		public static void PlacePictureOnTrackOfParticipant(IParticipant participant, Graphics g, string sideOfRoad)
		{
			int xPos = ImageCalculationX();
			int yPos = ImageCalculationY();

			if (sideOfRoad.Equals("Left"))
			{
				xPos += (imageSize / 3);
				yPos += (imageSize / 4);
			}
			else if (sideOfRoad.Equals("Right"))
			{
				xPos += (imageSize  / 2);
				yPos += (imageSize / 2);
			}

			if (participant.Equipment.IsBroken)
			{
				switch (participant.Name)
				{
					case "Mike":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player1Broken)), xPos, yPos);
						break;
					case "Jan":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player2Broken)), xPos, yPos);
						break;
					case "Thomas":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player3Broken)), xPos, yPos);
						break;
					case "Yasmine":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player4Broken)), xPos, yPos);
						break;
				}
			}
			else {
				switch (participant.Name)
				{
					case "Mike":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player1)), xPos, yPos);
						break;
					case "Jan":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player2)), xPos, yPos);
						break;
					case "Thomas":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player3)), xPos, yPos);
						break;
					case "Yasmine":
						g.DrawImage(RotateImage(PictureController.CloneImageFromCache(Player4)), xPos, yPos);
						break;
				}
			}
		}

		public static void PlaceAllPartcipants(Graphics g, Race race, Section section)
		{
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				if (participant.CurrentSection == section)
				{
					if (race.GetSectionData(section).Left == participant)
					{
						PlacePictureOnTrackOfParticipant(participant, g, "Left");
					}
					else if (race.GetSectionData(section).Right == participant)
					{
						PlacePictureOnTrackOfParticipant(participant, g, "Right");
					}
				}
			}
		}

		public static Bitmap RotateImage(Bitmap bitmap)
		{
			switch (direction)
			{
				case Direction.Right:
					bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
					break;
				case Direction.Left:
					bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
					break;
				case Direction.Up:
					bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
					break;
				case Direction.Down:
					bitmap.RotateFlip(RotateFlipType.RotateNoneFlipNone);
					break;
			}
			return bitmap;
		}


		public static void CalculateTrackSize()
		{
			TrackWidth = 2;
			TrackHeight = 2;
			foreach (Section section in Race.Track.Sections)
			{
				DetermineDirection(section.SectionTypes, direction);

				if (direction == Direction.Right)
				{
					TrackWidth++;
				}

				else if (direction == Direction.Down)
				{
					TrackHeight++;
				}
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
	}
}