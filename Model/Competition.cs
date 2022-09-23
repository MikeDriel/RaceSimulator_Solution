using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Competition
	{
		public List<IParticipant> Participants { get; set; }
		public Queue<Track> Tracks { get; set; }

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
				return Tracks.Dequeue();
			}
			else
			{
				return null;
			}
		}
    }
}
