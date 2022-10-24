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
			DetermineImageSources();
			
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
		private const string _StartGridHorizontal = "Graphics\\StartH.png";
		private const string _StraightHorizontal = "Graphics\\StraightH.png";
		private const string _finishHorizontal = "Graphics\\FinishH.png";
		private const string _LeftCornerHorizontal = "Graphics\\CornerNW.png";
		private const string _RightCornerHorizontal = "Graphics\\CornerSW.png";

		//Vertical
		private const string _StartGridVertical = "Graphics\\StartV.png";
		private const string _StraightVertical = "Graphics\\StraightV.png";
		private const string _finishVertical = "Graphics\\FinishV.png";
		private const string _LeftCornerVertical = "Graphics\\CornerSE.png";
		private const string _RightCornerVertical = "Graphics\\CornerNE.png";

		//Drivers
		private const string Player1 = "Graphics\\Player1.png";
		private const string Player2 = "Graphics\\Player2.png";
		private const string Player3 = "Graphics\\Player3.png";
		private const string Player4 = "Graphics\\Player4.png";

		//BrokenDrivers
		private const string Player1Broken = "Graphics\\Player1Broken.png";
		private const string Player2Broken = "Graphics\\Player2Broken.png";
		private const string Player3Broken = "Graphics\\Player3Broken.png";
		private const string Player4Broken = "Graphics\\Player4Broken.png";
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
				xPos += (imageSize / 5);
				yPos += (imageSize / 5);
			}
			else if (sideOfRoad.Equals("Right"))
			{
				xPos += (imageSize / 2);
				yPos += (imageSize / 2);
			}

			if (participant.Equipment.IsBroken)
			{
				g.DrawImage(RotateImage(PictureController.CloneImageFromCache(participant.ImagePathBroken)), xPos, yPos);
			}
			else
			{
				g.DrawImage(RotateImage(PictureController.CloneImageFromCache(participant.ImagePath)), xPos, yPos);
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

		public static void DetermineImageSources()
		{
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				switch (participant.TeamColor)
				{
					case TeamColors.Blue:
						participant.ImagePath = Player1;
						participant.ImagePathBroken = Player1Broken;
						break;
					case TeamColors.Purple:
						participant.ImagePath = Player2;
						participant.ImagePathBroken = Player2Broken;
						break;
					case TeamColors.Green:
						participant.ImagePath = Player3;
						participant.ImagePathBroken = Player3Broken;
						break;
					case TeamColors.Yellow:
						participant.ImagePath = Player4;
						participant.ImagePathBroken = Player4Broken;
						break;
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