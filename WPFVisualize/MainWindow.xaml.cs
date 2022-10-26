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
		private static Window window1;
		private static Window window2;
		public MainWindow()
		{


			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			WPFVisualize.Initialize(Data.CurrentRace);

			Data.CurrentRace.DriversChanged += OnDriversChanged;
			Data.CurrentRace.RaceEnd += OnRaceEnd;

			InitializeComponent();

			BitMapSourceDispatcher();

			this.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				window1 = new Window1();
				window2 = new Window2();
			}));
		}


		private void OnDriversChanged(object sender, DriversChangedEventArgs e)
		{
			BitMapSourceDispatcher();
		}

		private void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			Data.CurrentRace.CleanUp();

			if (Data.NextRace() != null)
			{
				WPFVisualize.Initialize(Data.CurrentRace);
				Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

				//Subscribes events 
				Data.CurrentRace.DriversChanged += OnDriversChanged;
				Data.CurrentRace.RaceEnd += OnRaceEnd;

				//Drawing track
				BitMapSourceDispatcher();
			}
			else
			{
				Application.Current.Dispatcher.Invoke((Action)delegate
				{
					window2.Show();
				});
			}
		}

		private void BitMapSourceDispatcher()
		{

			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = null;
				this.TrackImage.Source = WPFVisualize.DrawTrack(Data.CurrentRace.Track);
			}));

		}

		private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void MenuItem_OpenWindow1_Click(object sender, RoutedEventArgs e)
		{
			window1.Show();
		}

		private void MenuItem_OpenWindow2_Click(object sender, RoutedEventArgs e)
		{
			window2.Show();
		}

		private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}