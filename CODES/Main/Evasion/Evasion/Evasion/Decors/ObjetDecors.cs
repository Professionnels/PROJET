using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Evasion.Decors
{
    class ObjetDecors
    {
        private string fichier3D;
        private int type;
        private int identifiant;
        private Vector3 position;

        public string _fichier3D { get { return fichier3D; } }
        public int _type { get { return type; } }
        public int _identifiant { get { return identifiant; } }
        public Vector3 _position { get { return position; } }

        public ObjetDecors(string fichier3D, int type, int identifiant, Vector3 position)
        {
            this.fichier3D = fichier3D;
            this.type = type;
            this.identifiant = identifiant;
            this.position = position;
        }
        
    }
}
