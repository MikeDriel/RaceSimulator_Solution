using Controller;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		private string _trackName;
		public string TrackName { get { return _trackName; } set { _trackName = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler? PropertyChanged;



		public DataContext_MainWindow()
		{
			Data.CurrentRace.RaceEnd += OnRaceEnd;

			TrackName = Data.CurrentRace.Track.Name;
		}


		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			if (Data.NextRace() is null)
			{
				TrackName = "The competition is over!";
			}
			else
			{
				TrackName = Data.CurrentRace.Track.Name;
			}
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}