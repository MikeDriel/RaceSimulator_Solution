using Model;

namespace Controller
{
	public static class Data
	{
		static Competition Competition;
		

		public static void Initialize()
		{
		    Competition = new Competition();
			AddParticipants();
			AddTracks();
		}
		
		public static void AddParticipants()
		{
			Competition.Participants.Add(new Driver("Pieter", 0, new Car(10,10,10,false), TeamColors.Yellow)); ;
		}

		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("BloemendaalsTrack", new LinkedList<Section>()));
		}
	}
		
}