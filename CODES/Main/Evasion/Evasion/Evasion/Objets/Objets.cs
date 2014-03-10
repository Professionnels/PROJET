using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Evasion.Objets
{
    class Objets
    {
        private string fichier3D;
        private Vector3 position;

        public string _fichier3D { get { return fichier3D; } }
        public Vector3 _position { get { return position; } }
        
        public Objets(string fichier3D, Vector3 position)
        {
            this.fichier3D = fichier3D;
            this.position = position;
        }

        public virtual void Utiliser() 
        {

        }
    }
}
