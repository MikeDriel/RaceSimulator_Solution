﻿using Model;
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
	public class DataContext_CompetitionInfo : INotifyPropertyChanged
	{
		private BindingList<IParticipant> _driverData { get; set; }
		public BindingList<IParticipant> DriverData { get { return _driverData; } set { _driverData = value; OnPropertyChanged(); } }

		private BindingList<Track> _competitionData { get; set; }
		public BindingList<Track> CompetitionData { get { return _competitionData; } set { _competitionData = value; OnPropertyChanged(); } }

		private BindingList<LapTimeString> _lapTimeData { get; set; }
		public BindingList<LapTimeString> LapTimeData { get { return _lapTimeData; } set { _lapTimeData = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler? PropertyChanged;


		public DataContext_CompetitionInfo()
		{
			//OnRaceEnd Subscribe
			Data.CurrentRace.RaceEnd += OnRaceEnd;
			Data.CurrentRace.Finished += OnFinished;


			UpdateDriverData();
			UpdateCompetitionData(); UpdateLaptimes();
		}

		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			UpdateDriverData();
			UpdateCompetitionData();

			//OnRaceEnd Resubscribe
			Data.CurrentRace.RaceEnd += OnRaceEnd;
		}

		public void OnFinished(object sender, EventArgs e)
		{
			UpdateLaptimes();
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void UpdateDriverData()
		{

			List<IParticipant> leaderboardData = new List<IParticipant>();
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				leaderboardData.Add(participant);
			}
			DriverData = new BindingList<IParticipant>(leaderboardData.OrderByDescending(x => x.Points).ToList());
		}

		private void UpdateCompetitionData()
		{
			List<Track> competitionData = new List<Track>();
			foreach (Track track in Data.Competition.Tracks)
			{
				competitionData.Add(track);
			}
			CompetitionData = new BindingList<Track>(competitionData.ToList());
		}

		private void UpdateLaptimes()
		{
			List<LapTimeString> laptimes = new List<LapTimeString>();
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				laptimes.Add(new LapTimeString(participant));
			}
			LapTimeData = new BindingList<LapTimeString>(laptimes.ToList());
		}
	}
	
	public class LapTimeString
	{
		public string Value { get { return _value; } set { _value = value; } }
		private string _value { get; set; }

		public LapTimeString(IParticipant participant)
		{
			Value = participant.Name + " Finished in: " + participant.LapTime + " Seconds! ";
		}
	}
}