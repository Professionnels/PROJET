using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Evasion.Decors
{
    class Murs : ObjetDecors
    {
        public Murs(string fichier3D, int type, int identifiant, Vector3 position)
            :base(fichier3D, type, identifiant, position)
        {
            
        }
    }
}