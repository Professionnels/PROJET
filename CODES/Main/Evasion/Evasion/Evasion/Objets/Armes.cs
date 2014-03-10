using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Evasion.Objets
{
    
    class Armes : Objets
    {
        protected int portee;
        protected int degats;
        protected Son.Son bruit;
        protected int munition;

        public int _portee { get { return portee; } }
        public int _degats { get { return degats; } }
        public Son.Son _bruit { get { return bruit; } }
        public int _munition { get { return munition; } }

        public Armes(string fichier3D, Vector3 position, int portee, int degats, Son.Son bruit, int munition)
            : base(fichier3D, position)
        {
 
        }

        public int InfligerDegats(Personnage perso, int degats)
        {
            return (perso.ModifierVie(degats));
        }
    }

    class Pistolet : Armes
    {
        public Pistolet(Vector3 position, int portee, int degats, Son.Son bruit, int munition)
            : base("ImagePistolet3D", position, portee, degats, bruit, munition)
        {
 
        }
    }

    class Mitraillette : Armes
    {
        public Mitraillette(Vector3 position, int portee, int degats, Son.Son bruit, int munition)
            : base("ImageMitraillette3D", position, portee, degats, bruit, munition)
        {
 
        }
    }

    class Couteau : Armes
    {
        public Couteau(Vector3 position, int portee, int degats, Son.Son bruit, int munition)
            : base("ImageCouteau3D", position, portee, degats, bruit, -1)
        {
 
        }
    }
}
