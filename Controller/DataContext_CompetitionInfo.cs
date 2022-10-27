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
	public class DataContext_CompetitionInfo : INotifyPropertyChanged
	{
		private BindingList<IParticipant> _driverData { get; set; }
		public BindingList<IParticipant> DriverData { get { return _driverData; } set { _driverData = value; OnPropertyChanged(); } }

		private BindingList<Track> _competitionData { get; set; }
		public BindingList<Track> CompetitionData { get { return _competitionData; } set { _competitionData = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler? PropertyChanged;


		public DataContext_CompetitionInfo()
		{
			//OnRaceEnd Subscribe
			Data.CurrentRace.RaceEnd += OnRaceEnd;

			

			UpdateDriverData();
			UpdateCompetitionData();
		}

		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{
			UpdateDriverData();

			//OnRaceEnd Resubscribe
			Data.CurrentRace.RaceEnd += OnRaceEnd;
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

	}
}