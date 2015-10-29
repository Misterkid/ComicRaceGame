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
        //Dictionary with soundname and the actual sound!
        private static Dictionary<string, jSoundFile> geluidsDictionary = new Dictionary<string, jSoundFile>();

        //play sound (depending on the soundname)
        public static void PlaySound(string soundName)
        {
            geluidsDictionary[soundName].Play();
        }
        //Play the sound looping by sound name
        public static void PlaySoundLooping(string soundName)
        {
            geluidsDictionary[soundName].PlayLooping();
        }
        //stop the sound by soundname
        public static void StopSound(string soundName)
        {
            geluidsDictionary[soundName].Stop();
        }
        //stop all sounds
        public static void StopAllSounds()
        {
            for(int i = 0; i < geluidsDictionary.Count; i++)
            {
                geluidsDictionary.ElementAt(i).Value.Stop();
            }
        }
        //add a sound by sound name if not excist.
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
