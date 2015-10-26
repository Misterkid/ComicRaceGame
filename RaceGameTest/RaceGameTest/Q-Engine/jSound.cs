using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace RaceGameTest.Q_Engine
{
    class jSound
    {
        private static Dictionary<string, SoundPlayer> geluidsDictionary = new Dictionary<string, SoundPlayer>();

        public static void PlaySound(string soundName)
        {
            geluidsDictionary[soundName].PlaySync();
        }
        public static void AddSound(string soundName, string soundPath)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundPath);
            geluidsDictionary.Add(soundName, player);
        }	
    }
}
