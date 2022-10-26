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

		private BindingList<string> _competitionData { get; set; }
		public BindingList<string> CompetitionData { get { return _competitionData; } set { _competitionData = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler? PropertyChanged;


		public DataContext_CompetitionInfo()
		{
			List<IParticipant> leaderboardData = new List<IParticipant>();
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				leaderboardData.Add(participant);
			}
			DriverData = new BindingList<IParticipant>(leaderboardData.ToList());

			List<string> competitionData = new List<string>();
			foreach (Track track in Data.Competition.Tracks)
			{
				competitionData.Add(track.Name);
			}
			CompetitionData = new BindingList<string>(competitionData.ToList());
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}