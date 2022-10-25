using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;

namespace WPFApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Window window1;
		private Window window2;
		public MainWindow()
		{


			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);



			WPFVisualize.Initialize(Data.CurrentRace);

			Data.CurrentRace.DriversChanged += OnDriversChanged;
			Data.CurrentRace.RaceEnd += OnRaceEnd;

			Data.CurrentRace.Start();
			InitializeComponent();
			TrackImage.HorizontalAlignment = HorizontalAlignment.Left;
			TrackImage.VerticalAlignment = VerticalAlignment.Top;

			TrackImage.Width = WPFVisualize.TrackWidth / 2;
			TrackImage.Height = WPFVisualize.TrackHeight / 2;

			this.TrackImage.Source = null;
			this.TrackImage.Source = WPFVisualize.DrawTrack(Data.CurrentRace.Track);
		}


		private void OnDriversChanged(object sender, DriversChangedEventArgs e)
		{
			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = null;
				this.TrackImage.Source = WPFVisualize.DrawTrack(Data.CurrentRace.Track);

			}));
		}

		private void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			Data.CurrentRace.CleanUp();

			if (Data.NextRace() != null)
			{
				WPFVisualize.Initialize(Data.CurrentRace);
				Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

				//Clearing cache
				PictureController.EmptyCache();

				//Subscribes events 
				Data.CurrentRace.DriversChanged += OnDriversChanged;
				Data.CurrentRace.RaceEnd += OnRaceEnd;

				//Initializes race
				WPFVisualize.Initialize(Data.CurrentRace);

				//Drawing track
				this.TrackImage.Dispatcher.BeginInvoke(
				DispatcherPriority.Render,
				new Action(() =>
				{
					this.TrackImage.Source = null;
					this.TrackImage.Source = WPFVisualize.DrawTrack(Data.CurrentRace.Track);

				}));

				//start timer
				Data.CurrentRace.Start();
			}
			else
			{
				//Dispatcher for closing the main window and opening scores
				Application.Current.Dispatcher.Invoke((Action)delegate
				{
					window2 = new Window2();
					window2.Show();
					this.Close();
				});
			}
		}


		private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void MenuItem_OpenWindow1_Click(object sender, RoutedEventArgs e)
		{
			window1 = new Window1();
			window1.Show();
		}

		private void MenuItem_OpenWindow2_Click(object sender, RoutedEventArgs e)
		{
			window2 = new Window2();
			window2.Show();
		}
	}
}