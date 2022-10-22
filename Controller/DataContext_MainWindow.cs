using Controller;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		private string _trackName;
		private string _metersMoved;
		private List<IParticipant> _participants;

		public List<IParticipant> Participants { get { return _participants; } set { _participants = value; OnPropertyChanged(); } }

		public string TrackName { get { return _trackName; } set { _trackName = value; OnPropertyChanged(); } }

		public string metersMoved { get { return _metersMoved; } set { _metersMoved = value; OnPropertyChanged(); } }





		public event PropertyChangedEventHandler? PropertyChanged;



		public DataContext_MainWindow()
		{
			//Data.CurrentRace.DriversChanged += OnDriversChanged;
			Data.CurrentRace.RaceEnd += OnRaceEnd;

			TrackName = Data.CurrentRace.Track.Name;
			Participants = Data.CurrentRace.Participants;
		}


		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			TrackName = Data.CurrentRace.Track.Name;
			Participants = Data.CurrentRace.Participants;
		}

		private void OnDriversChanged(object sender, DriversChangedEventArgs e)
		{

		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}