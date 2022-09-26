using Controller;
using Model;
using RaceSimulator_Project;
using System.Net.Mail;

Data.Initialize();
Data.NextRace();
Data.NextRace();
Data.CurrentRace.PlaceDriversOnStart(Data.CurrentRace.Track, Data.CurrentRace.Participants);

Data.CurrentRace.DriversChanged += Visualisatie.OnDriversChanged;

Visualisatie.Initialize(Data.CurrentRace);

Visualisatie.DrawTrack(Data.CurrentRace.Track);



for (; ; )
{
	Thread.Sleep(100);
}