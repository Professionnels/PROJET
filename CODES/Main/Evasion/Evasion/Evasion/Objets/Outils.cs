using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Evasion.Objets
{
    class Outils : Objets
    {
        private int quantite;

        public int _quantite { get { return quantite; } }
  
        public Outils(string fichier3D, Vector3 position, int portee, int quantite, Son.Son bruit)
            : base(fichier3D, position)
        {
 
        }
    }
}
