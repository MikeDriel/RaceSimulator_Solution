using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
			RandomizeEquipment();
			Start();
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
				participant.Equipment.Quality = _random.Next(5, 11);
				participant.Equipment.Performance = _random.Next(5, 11);
			}
		}


		//Places drivers on the startgrid and behind it until no more participants are left in the list
		public void PlaceDriversOnStart(Track track, List<IParticipant> participants)
		{
			// Index to keep track of on which section the foreach loop is currently
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

							participants[i].CurrentSection = track.Sections.ElementAt(index - (i / 2));
						}

						if (sectionData.Right == null && participants.Count % 2 == 0)
						{
							sectionData.Right = participants[i + 1];

							participants[i + 1].CurrentSection = track.Sections.ElementAt(index - (i / 2));


						}
					}
					return;
				}
				index++;
			}
		}

		public void CheckForMoveDriver()
		{
			foreach (IParticipant participant in Participants)
			{
				//Calculate the distance the driver can move
				participant.DistanceCovered += participant.Equipment.Speed * participant.Equipment.Performance;

				if (participant.DistanceCovered >= 100)
				{
					participant.DistanceCovered += -100;
					MoveDriver(participant);
				}
			}

		}

		public void MoveDriver(IParticipant participant)
		{
			int i = 0;
			foreach (Section section in Track.Sections)
			{
				if (section == participant.CurrentSection)
				{

					//This part checks the fist section of the track with the drivers
					SectionData sectionData = GetSectionData(section);

					if (sectionData.Left == participant)
					{
						sectionData.Left = null;
					}
					else if (sectionData.Right == participant)
					{
						sectionData.Right = null;
					}

					if (Track.Sections.Count <= (i + 1))
					{
						i = -1;
					}

					//This part checks the second section behind the first one of the track with the drivers
					SectionData nextSectionData = GetSectionData(Track.Sections.ElementAt(i + 1));

					if (nextSectionData.Left == null)
					{
						nextSectionData.Left = participant;
					}
					else if (nextSectionData.Right == null)
					{
						nextSectionData.Right = participant;
					}
					participant.CurrentSection = Track.Sections.ElementAt(i + 1);

					//Checks if the drivers go over a finish section for the x'th amount of time and then makes them go poof to the shadow realm
					if (CheckFinish(participant) == true)
					{
						participant.CurrentSection = null;
						if (nextSectionData.Left == participant)
						{
							nextSectionData.Left = null;
						}
						else if (nextSectionData.Right == participant)
						{
							nextSectionData.Right = null;
						}
					}
					return;
				}
				i++;
			}

		}

		//Checks if drivers touch the finish
		public Boolean CheckFinish(IParticipant participant)
		{
			if (participant.CurrentSection.SectionTypes == SectionType.Finish)
			{
				participant.Loops += 1;
				//Number determines the amount of laps the drivers have to do
				if (participant.Loops == 1)
				{
					return true;
				}
				else return false;
			}
			else return false;
		}

		//Checks if all drivers are finished
		public Boolean CheckIfAllDriversFinished()
		{
			foreach (IParticipant participant in Participants)
			{
				if (participant.CurrentSection != null)
				{
					return false;
				}
			}
			return true;
		}

		//TimerEvent
		public void OnTimedEvent(object source, EventArgs e)
		{
			int times = 0;
			if (CheckIfAllDriversFinished() == true && times == 0)
			{
				CleanUp();
				times++;
			}
			else
			{
				CheckForMoveDriver();
				DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
			}
		}

		//Start timer
		public void Start()
		{
			_timer.Start();
		}

		public void Stop()
		{
			_timer.Stop();
		}

		//This function will clean up the last eventHandeler reference so the garbage collector can clean up the memory
		public void CleanUp()
		{
			Console.Clear();
			Console.WriteLine("Cleaning up");
			Stop();
			DriversChanged = null;
			Console.WriteLine("Cleaning done");
			GC.Collect(0);
		}
	}
}