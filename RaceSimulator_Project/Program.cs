using Controller;
using Model;
using System.Net.Mail;

Data.Initialize();
Data.NextRace();
Console.WriteLine(Data.CurrentRace.Track.Name);
Data.NextRace();
Console.WriteLine(Data.CurrentRace.Track.Name);
Data.NextRace();
Console.WriteLine(Data.CurrentRace.Track.Name);


for (; ; )
{
	Thread.Sleep(100);
}