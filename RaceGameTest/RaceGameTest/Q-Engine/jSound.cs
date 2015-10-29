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
        private static Dictionary<string, jSoundFile> geluidsDictionary = new Dictionary<string, jSoundFile>();

        public static void PlaySound(string soundName)
        {
            geluidsDictionary[soundName].Play();
        }
        public static void PlaySoundLooping(string soundName)
        {
            geluidsDictionary[soundName].PlayLooping();
        }
        public static void StopSound(string soundName)
        {
            geluidsDictionary[soundName].Stop();
        }
        public static void StopAllSounds()
        {
            for(int i = 0; i < geluidsDictionary.Count; i++)
            {
                geluidsDictionary.ElementAt(i).Value.Stop();
            }
        }
        public static void AddSound(string soundName, string soundPath,float volume)
        {
            if (!geluidsDictionary.ContainsKey(soundName))
            {
                jSoundFile jSoundFile = new jSoundFile(soundPath, volume);
                geluidsDictionary.Add(soundName, jSoundFile);
            }
        }	
    }
}
