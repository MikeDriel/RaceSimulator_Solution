using Controller;
using Model;
using System.Net.Mail;

Data.Initialize();
Data.NextRace();
Console.WriteLine(Data.currentTrack.Name);


for (; ; )
{
	Thread.Sleep(100);
}