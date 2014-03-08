using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Evasion.Son
{
    static class ChargerSon
    {
        public static SoundEffect son_bouton;
        public static SoundEffect son_pistolet;
        public static SoundEffect son_rafale;
        public static SoundEffect son_coup;
        public static SoundEffect son_cri;
        public static SoundEffect son_pause;

        public static Song theme1;
        public static Song theme2;

        static public void Init(ContentManager cm)
        {
            son_bouton = cm.Load<SoundEffect>("son_bouton");
            son_pistolet = cm.Load<SoundEffect>("son_pistolet");
            son_rafale = cm.Load<SoundEffect>("son_rafale");
            son_coup = cm.Load<SoundEffect>("son_coup");
            son_cri = cm.Load<SoundEffect>("son_cri");
            son_pause = cm.Load<SoundEffect>("son_pause");

            theme1 = cm.Load<Song>("theme1");
            theme2 = cm.Load<Song>("theme2");
        }


    }
}
