using Controller;
using Model;
using RaceSimulator_Project;
using System.Net.Mail;


Data.Initialize();
Data.NextRace();

Visualisatie.Initialize(Data.CurrentRace);

Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

Visualisatie.DrawTrack(Data.CurrentRace.Track);

//Data.CurrentRace.Start();

for (; ; )
{
	Thread.Sleep(500);
}