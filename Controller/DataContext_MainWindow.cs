using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		public List<IParticipant> Participants { get; set; }

		public string TrackName { get; set; }

		public string metersMoved { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		

		public DataContext_MainWindow()
		{
			Data.CurrentRace.DriversChanged += OnDriversChanged;
			Data.CurrentRace.RaceEnd += OnRaceEnd;
			PropertyChanged += OnPropertyChanged;

			TrackName = Data.CurrentRace.Track.Name;
			Participants = Data.CurrentRace.Participants;
		}

		
		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			Data.CurrentRace.DriversChanged += OnDriversChanged;
			Data.CurrentRace.RaceEnd += OnRaceEnd;
			
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrackName"));
		}

		private void OnDriversChanged(object sender, DriversChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MetersMoved1"));
		}

		public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "TrackName":
					TrackName = Data.CurrentRace.Track.Name;
					break;
				case "Participants":
					Participants = Data.CurrentRace.Participants;
					break;
				case "MetersMoved1":
					metersMoved = Data.CurrentRace.Participants[0].DistanceCovered.ToString();
					break;
			}
		}
	}
}