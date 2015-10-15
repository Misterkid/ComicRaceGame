using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Application
{
	public class EmptyClass
	{
		public static void Main ()
		{
		geluid("rem");
		}
		public static void geluid(string gebeurtenis)
		{
		Dictionary<string, string> geluidsDictionary = new Dictionary<string, string>();
		geluidsDictionary.Add("bots", "bots.wav");
		geluidsDictionary.Add("rem", "remmen.wav");
		geluidsDictionary.Add("vroem", "vroem.wav");
		
		string aanvraag = geluidsDictionary[gebeurtenis];
		System.Media.SoundPlayer player = new System.Media.SoundPlayer(aanvraag);
		player.Play();
		Console.ReadLine();
		}	
	}
}
