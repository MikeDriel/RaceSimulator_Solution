using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		//public Race CurrentRace { get; set; }
		
		//public List<ParticipantLapTime> LapTimes { get; private set; }
		//public List<ParticipantSectionTime> SectionTimes { get; private set; }
		public List<IParticipant> Participants { get; set; }
		public string BestSectionTime { get; set; }
		public string BestLapTime { get; set; }
		public string TrackName { get; set; }

		public DataContext_MainWindow()
		{
			Data.CurrentRace.DriversChanged += OnDriversChanged;
			Data.CurrentRace.RaceEnd += OnRaceEnd;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			
		}

		private void OnDriversChanged(object sender, DriversChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(" "));
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("trackname"))
			{
				
			}
			else if (e.PropertyName.Equals("speed"))
			{

			}
		}
	}
}

