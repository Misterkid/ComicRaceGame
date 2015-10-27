using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.InteropServices;
using System.Timers;
using IrrKlang;

using System.Diagnostics;

namespace RaceGameTest.Q_Engine
{
    class jSoundFile
    {
        private float volume = 1;
        ISoundEngine engine;
        private string _path;
        public jSoundFile(string path, float vlm)
        {
            // start up the engine
            engine = new ISoundEngine();
            _path = path;
            volume = vlm;
        }
        public void Play()
        {
            if (!engine.IsCurrentlyPlaying(_path))
            {
                engine.SoundVolume = volume;
                engine.Play2D(_path);
            }
        }
        public void Stop()
        {
            engine.StopAllSounds();
        }
        public void PlayLooping()
        {
            engine.Play2D(_path, true, false, StreamMode.Streaming,true);
        }
    }
}
