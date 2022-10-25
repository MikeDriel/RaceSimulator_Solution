using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Competition
	{
		public List<IParticipant> Participants { get; set; }
		public Queue<Track> Tracks { get; set; }
		public IParticipant Winner { get; set; }

		public event EventHandler CompetitionEnd;

		//Constructor for competition
		public Competition()
		{
			Participants = new List<IParticipant>();
			Tracks = new Queue<Track>();
		}

		//Queues the next track in the competition
		public Track NextTrack()
        {
			if (Tracks.Count > 0)
			{
				CompetitionEnd.Invoke(this, new EventArgs());
				return Tracks.Dequeue();
			}
			else
			{
				Winner = Participants.OrderByDescending(x => x.Points).First();
				return null;
			}
		}
    }
}
