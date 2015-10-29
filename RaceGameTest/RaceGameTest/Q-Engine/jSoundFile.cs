using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.InteropServices;
using System.Timers;
using IrrKlang;//Using the irrklang sound libary. The Windows c# libaries sucks!

using System.Diagnostics;

namespace RaceGameTest.Q_Engine
{
    class jSoundFile
    {
       // private float volume = 1;//Sound volume
        ISoundEngine engine;//the sound engine(I use it mutiple times.... Could be used ones.) No time to test
        private string _path;//File path

        //constructor. sound need path and volume
        public jSoundFile(string path, float vlm)
        {
            // start up the engine
            engine = new ISoundEngine();//sound engine!
            //volume = vlm;//set volume
            engine.SoundVolume = vlm;//^
            _path = path;//Set file path
        }
        public void Play()
        {
            if (!engine.IsCurrentlyPlaying(_path))//is the sound not playing?
            {
                ISound sound = engine.Play2D(_path);//play
            }
        }
        //stop sound
        public void Stop()
        {
            engine.StopAllSounds();
        }
        //play sound looping if not playing
        public void PlayLooping()
        {
            if (!engine.IsCurrentlyPlaying(_path))
            {
                engine.Play2D(_path, true, false, StreamMode.AutoDetect, true);
            }
        }
    }
}
