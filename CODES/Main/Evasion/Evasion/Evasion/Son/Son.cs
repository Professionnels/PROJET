using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Evasion.Son
{

    class Son
    {
        private SoundEffect son;
        private Song song;
        private bool musique;

        public Son(SoundEffect son)
        {
            this.son = son;
            musique = false;
        }

        public Son(Song song)
        {
            this.song = song;
            musique = true;
        }

        public void Play()
        {
            if (!musique)
                son.Play();
            /*else
                MediaPlayer.Play(song);*/
        }
    }
}
