using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class RaceEndEventArgs : EventArgs
	{
		public List<IParticipant> Participants { get; set; }

		public RaceEndEventArgs(List<IParticipant> participants)
		{
			Participants = participants;
		}
	}
}
