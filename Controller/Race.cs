using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Controller
{
	public class Race
	{
		
		public event EventHandler<DriversChangedEventArgs> DriversChanged;
		
		public Track Track { get; set; }
		public List<IParticipant> Participants { get; set; }
		public DateTime StartTime { get; set; }
		private Random _random { get; set; }
		private Dictionary<Section, SectionData> _positions { get; set; }
		private System.Timers.Timer _timer { get; set; }

		//Constructor for Race
		public Race(Track track, List<IParticipant> participants)
		{
			Track = track;
			Participants = participants;
			_random = new Random(DateTime.Now.Millisecond);
			StartTime = new DateTime();
			_positions = new Dictionary<Section, SectionData>();
			_timer = new System.Timers.Timer(500);
			_timer.Elapsed += OnTimedEvent;
		}

		//Gets the sectiondata for the given section, if it doesn't exist, it creates it
		public SectionData GetSectionData(Section section)
		{
			if (!_positions.ContainsKey(section))
			{
				_positions.Add(section, new SectionData());
			}
			return _positions[section];
		}

		//Randomizes equipment for all participants
		public void RandomizeEquipment()
		{
			foreach (IParticipant participant in Participants)
			{
				participant.Equipment.Quality = _random.Next(1, 10);
				participant.Equipment.Performance = _random.Next(1, 10);
			}
		}


		//Places drivers on the startgrid and behind it until no more participants are left in the list
		public void PlaceDriversOnStart(Track track, List<IParticipant> participants)
		{
			int index = 0;
			foreach (Section section in track.Sections)
			{
				if (section.SectionTypes == SectionType.StartGrid)
				{
					for (int i = 0; i < participants.Count; i += 2)
					{
						if ((index - i / 2) < 0)
						{
							index = track.Sections.Count;
						}

						SectionData sectionData = GetSectionData(track.Sections.ElementAt(index - (i / 2)));

						if (sectionData.Left == null)
						{
							sectionData.Left = participants[i];
						}

						if (sectionData.Right == null && participants.Count % 2 == 0)
						{
							sectionData.Right = participants[i + 1];
						}
					}
					return;
				}
				index++;
			}
		}
		
		
		//TimerEvent
		public void OnTimedEvent(object source, EventArgs e)
		{
			DriversChanged.Invoke(this, EventArgs.Empty);
		}

		//Start timer
		public void Start()
		{
			_timer.Start();
		}
	}
}