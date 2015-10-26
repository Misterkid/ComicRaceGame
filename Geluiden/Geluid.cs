using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Application
{
	public class EmptyClass
	{
		Dictionary<string, string> geluidsDictionary = new Dictionary<string, SoundPlayer>();
		public static void Main ()
		{
			//geluid("rem");
			AddSound("rem","remmen.wav");
			AddSound("rem2","remmen.wav");
			AddSound("rem3","remmen.wav");
			
			PlaySound("rem");
		}
		public static void PlaySound(string soundName)
		{
			geluidsDictionary[soundName].Play();
		}
		public static void AddSound(string soundName, string soundPath)
		{
			System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundPath);
			geluidsDictionary.Add(soundName, player);
		}	
	}
}
