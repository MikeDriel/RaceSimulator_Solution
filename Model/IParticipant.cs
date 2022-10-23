namespace Model
{
	public interface IParticipant
	{
		public string Name { get; set; }
		int Points { get; set; }
		IEquipment Equipment { get; set; }
		TeamColors TeamColors { get; set; }
		public Section CurrentSection { get; set; }
		public int DistanceCovered { get; set; }
		public int Laps { get; set; }

	}
	
	public enum TeamColors
	{
		Red,
		Green,
		Yellow,
		Grey,
		Blue,
		Purple
	}
}