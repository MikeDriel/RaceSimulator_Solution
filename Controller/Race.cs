using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
	public class Race
	{
		Track Track;
		List<IParticipant> Participants;
		DateTime StartTime;
		private Random _random;
		private Dictionary<Section, SectionData> _positions;

		public Race(Track Track, List<IParticipant> Participants)
		{
			Track = Track;
			Participants = Participants;
			_random = new Random(DateTime.Now.Millisecond);
		}

		public SectionData GetSectionData(Section section)
		{
			if (!_positions.ContainsKey(section))
			{
				{
					_positions.Add(section, new SectionData());
				}
			}
			return _positions[section];
		}
		
		public void RandomizeEquipment()
		{
			foreach (IParticipant participant in Participants)
			{
				participant.Equipment.Quality = _random.Next(1, 100);
				participant.Equipment.Performance = _random.Next(1, 100);
			}
		}
	}
}
