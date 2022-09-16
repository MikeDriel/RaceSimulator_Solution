using Controller;
using Model;
using RaceSimulator_Project;
using System.Net.Mail;

Data.Initialize();
Data.NextRace();
Visualisatie.Initialize();
Visualisatie.DrawTrack(Data.CurrentRace.Track);



for (; ; )
{
	Thread.Sleep(100);
}