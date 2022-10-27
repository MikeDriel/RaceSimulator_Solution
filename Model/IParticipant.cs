using System.ComponentModel;

namespace Model
{
	public interface IParticipant : INotifyPropertyChanged
	{
		public string Name { get; set; }
		int Points { get; set; }
		IEquipment Equipment { get; set; }
		TeamColors TeamColor { get; set; }
		public Section CurrentSection { get; set; }
		public int DistanceCovered { get; set; }
		public int Laps { get; set; }
		public bool IsFinished { get; set; }
		public string ImagePath { get; set; }
		public string ImagePathBroken { get; set; }
		public double LapTime { get; set; }

}
	
	public enum TeamColors
	{
		Green,
		Yellow,
		Blue,
		Purple
	}
}