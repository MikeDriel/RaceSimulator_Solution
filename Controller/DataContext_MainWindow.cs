using Controller;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		private string _raceName = "";
		public string RaceName { get { return _raceName; } set { _raceName = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler? PropertyChanged;

		public DataContext_MainWindow()
		{
			Data.CurrentRace.RaceEnd += OnRaceEnd;

			RaceName = Data.CurrentRace.Track.Name;
		}

		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			RaceName = Data.CurrentRace.Track.Name;
			Data.CurrentRace.RaceEnd += OnRaceEnd;
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}