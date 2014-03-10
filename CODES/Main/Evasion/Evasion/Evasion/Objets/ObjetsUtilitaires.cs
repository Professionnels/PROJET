using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Evasion.Objets
{
    class ObjetsUtilitaires : Objets
    {
        public ObjetsUtilitaires(string fichier3D, Vector3 position, int portee, int munition, Son.Son bruit)
            : base(fichier3D, position)
        {
 
        }
    }
}
