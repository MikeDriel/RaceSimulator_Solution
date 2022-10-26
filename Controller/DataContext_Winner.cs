using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class DataContext_Winner : INotifyPropertyChanged
	{
		private string _winnerText = "There is no winner yet..";
		private string _winnerURL { get; set; }
		public string WinnerText { get { return _winnerText; } set { _winnerText = value; OnPropertyChanged(); } }
		public IParticipant? Winner { get; set; }
		public string WinnerURL { get { return _winnerURL; } set { _winnerURL = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler? PropertyChanged;


		public DataContext_Winner()
		{
			Data.Competition.CompetitionEnd += OnCompetitionEnd;
		}
		
		public void OnCompetitionEnd(object? sender, EventArgs e)
		{
			Winner = Data.Competition.Winner;
			WinnerURL = Winner.ImagePath;
			WinnerText = "The winner is: " + Winner.Name + " with " + Winner.Points + " points!";
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
