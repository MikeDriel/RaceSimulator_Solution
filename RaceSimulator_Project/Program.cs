using Controller;
using Model;
using RaceSimulator_Project;
using System.Net.Mail;


Data.Initialize();
Data.NextRace();
Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

Visualisatie.Initialize(Data.CurrentRace);

//Data.CurrentRace.DriversChanged += Visualisatie.OnDriversChanged;
//Data.CurrentRace.RaceEnd += Visualisatie.OnRaceEnd;

Visualisatie.DrawTrack(Data.CurrentRace.Track);

//Data.CurrentRace.Start();

for (; ; )
{
	Thread.Sleep(500);
}