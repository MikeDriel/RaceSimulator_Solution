namespace Model
{
	public interface IParticipant
	{
		public string Name { get; set; }
		int Points { get; set; }
		IEquipment Equipment { get; set; }
		TeamColors TeamColors { get; set; }

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