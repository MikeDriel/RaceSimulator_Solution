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
		public event EventHandler<RaceEndEventArgs> RaceEnd;


		public Track Track { get; set; }
		public List<IParticipant> Participants { get; set; }
		public DateTime StartTime { get; set; }
		private Random _random { get; set; }
		private Dictionary<Section, SectionData> _positions { get; set; }
		private System.Timers.Timer _timer { get; set; }

		private int AmountOfLoops = 3;

		private int counter = 1;


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
			Start();
			RandomizeEquipment();
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
		private void RandomizeEquipment()
		{
			foreach (IParticipant participant in Participants)
			{
				participant.Equipment.Quality = _random.Next(5, 11);
				participant.Equipment.Performance = _random.Next(5, 11);
				UpdateSpeed(participant);
				participant.IsFinished = false;
			}
		}

		//Updates Speed Variable
		private void UpdateSpeed(IParticipant participant)
		{
			participant.Equipment.Speed = participant.Equipment.Quality * participant.Equipment.Performance;
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

		private void CheckForMoveDriver()
		{
			foreach (IParticipant participant in Participants)
			{
				if (participant.IsFinished == false)
				{
					if (participant.Equipment.IsBroken == false)
					{
						//Calculate the distance the driver can move
						participant.DistanceCovered += participant.Equipment.Speed;

						if (participant.DistanceCovered >= 100)
						{
							participant.DistanceCovered -= 100;
							MoveDriver(participant);
						}
					}
				}
			}
		}

		private void MoveDriver(IParticipant participant)
		{
			int i = 0;
			foreach (Section section in Track.Sections)
			{
				//Checks if overtaking is possible
				if (section == participant.CurrentSection)
				{
					//checks if equipment is broken
					if (participant.Equipment.IsBroken == true)
					{
						return;
					}

					//This part checks the fist section of the track with the drivers
					SectionData sectionData = GetSectionData(section);

					//Makes sure out of bounds in the tracklist doesnt happen
					if (Track.Sections.Count <= (i + 1))
					{
						i = -1;
					}

					//This part checks the second section behind the first one of the track with the drivers
					SectionData nextSectionData = GetSectionData(Track.Sections.ElementAt(i + 1));


					if (nextSectionData.Left == null || nextSectionData.Right == null)
					{
						//check for this section
						//TODO: REFACTOR THIS
						if (sectionData.Left == participant)
						{
							sectionData.Left = null;
						}
						else if (sectionData.Right == participant)
						{
							sectionData.Right = null;
						}

						//check for next section
						if (nextSectionData.Left == null)
						{
							nextSectionData.Left = participant;
						}
						else if (nextSectionData.Right == null)
						{
							nextSectionData.Right = participant;
						}
						//else Overtaking(participant, nextSectionData.Right);
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

							//If there are no more drivers on the track it will cleanup and start the next race
							if (CheckIfAllDriversFinished() == true)
							{
								RaceEnd.Invoke(this, new RaceEndEventArgs(Data.CurrentRace.Participants));
							}
						}
					}
					//Sets distance to 100 if inhalen can't 
					else
					{
						participant.DistanceCovered = 100;
					}
					return;
				}
				i++;
			}
		}

		//Checks if drivers touch the finish
		private Boolean CheckFinish(IParticipant participant)
		{
			if (participant.CurrentSection.SectionTypes == SectionType.Finish)
			{
				participant.Laps += 1; //Add lap
				
				//Number determines the amount of laps the drivers have to do
				if (participant.Laps >= AmountOfLoops)
				{
					participant.Points += (5 / counter); //Add point
					counter++;
					participant.IsFinished = true;
					return true;
				}
				else return false;
			}
			else return false;
		}

		//Checks if all drivers are finished
		private Boolean CheckIfAllDriversFinished()
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

		//Checks for Crash
		private void CheckForCrash()
		{
			foreach (IParticipant participant in Participants)
			{
				//check for recover
				if (participant.Equipment.IsBroken == true)
				{
					int recover = _random.Next(1, 20);
					if (recover <= 5)
					{
						if (participant.Equipment.Quality == 1)
						{
							participant.Equipment.Quality = 1;
							participant.Equipment.IsBroken = false;
						}
						else
						{
							participant.Equipment.Quality -= 1;
							participant.Equipment.IsBroken = false;
							UpdateSpeed(participant);
						}
					}
				}

				//check for break
				int isBreak = _random.Next(1, 40);
				if (isBreak == 1)
				{
					participant.Equipment.IsBroken = true;
				}
			}
		}

		//Start timer
		public void Start()
		{
			_timer.Start();
		}

		//Stops timer
		public void Stop()
		{
			_timer.Stop();
		}

		//TimerEvent
		public void OnTimedEvent(object source, EventArgs e)
		{
			CheckForMoveDriver();
			CheckForCrash();
			DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
		}


		//This function will clean up the last eventHandeler reference so the garbage collector can clean up the memory
		public void CleanUp()
		{
			foreach (IParticipant participant in Participants)
			{
				participant.CurrentSection = null;
				participant.DistanceCovered = 0;
				participant.Laps = 0;
			}

			_timer.Stop();
			_timer.Dispose();
			DriversChanged = null;
			//Data.CurrentRace = null;
			GC.Collect(0);


			Console.WriteLine("Next race in 3..");
			Thread.Sleep(1000);
			Console.WriteLine("Next race in 2..");
			Thread.Sleep(1000);
			Console.WriteLine("Next race in 1..");
			Thread.Sleep(1000);
		}
	}
}