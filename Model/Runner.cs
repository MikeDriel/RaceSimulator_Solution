using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Runner : IParticipant
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private int _distanceCovered;
		private int _laps;
		private int _points;
		private string _name;
		private bool _isFinished;
		private double _lapTime;



		public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
		public int Points { get { return _points; } set { _points = value; OnPropertyChanged(); } }
		public IEquipment Equipment { get; set; }
		public TeamColors TeamColor { get; set; }
		public Section CurrentSection { get; set; }
		public int DistanceCovered { get { return _distanceCovered; } set { _distanceCovered = value; OnPropertyChanged(); } }
		public int Laps { get { return _laps; } set { _laps = value; OnPropertyChanged(); } }
		public bool IsFinished { get { return _isFinished; } set { _isFinished = value; OnPropertyChanged(); } }
		public string ImagePath { get; set; }
		public string ImagePathBroken { get; set; }
		public double LapTime { get { return _lapTime; } set { _lapTime = value; OnPropertyChanged(); } }

		//Constructor to input static values of driver
		public Runner(string name, int points, IEquipment equipment, TeamColors teamColors)
		{
			Name = name;
			Points = points;
			Equipment = equipment;
			TeamColor = teamColors;
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}