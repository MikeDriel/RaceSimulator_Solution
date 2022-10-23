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
	public class DataContext_Window1 : INotifyPropertyChanged
	{
		private BindingList<IParticipant> _driverData { get; set; }
		public BindingList<IParticipant> DriverData { get {return _driverData;} set { _driverData = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler? PropertyChanged;
		
		
		public DataContext_Window1()
		{
				List<IParticipant> leaderboardData = new List<IParticipant>();
				foreach (IParticipant participant in Data.CurrentRace.Participants)
				{
					leaderboardData.Add(participant);
				}
				DriverData = new BindingList<IParticipant>(leaderboardData.ToList());
		}
	
		public void OnRaceEnd(object sender, RaceEndEventArgs e)
		{

		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}