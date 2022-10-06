using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			WPFVisualize.Initialize(Data.CurrentRace);
			Data.CurrentRace.Start();

			TrackImage.HorizontalAlignment = HorizontalAlignment.Left;
			TrackImage.VerticalAlignment = VerticalAlignment.Top;
			
			TrackImage.Width = WPFVisualize.XimageScale * WPFVisualize.imageSize;
			TrackImage.Height = WPFVisualize.YimageScale * WPFVisualize.imageSize;
			
			this.TrackImage.Source = null;
			this.TrackImage.Source = WPFVisualize.DrawTrack(Data.CurrentRace.Track);


			
		}
	}
}
