using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion.Decors
{
    class Murs
    {
        private string fichier3D;
        private int type;
        private int identifiant;

        public Murs() { }
        public Murs(string fichier3D, int type, int identifiant)
        {
            this.fichier3D = fichier3D;
            this.type = type;
            this.identifiant = identifiant;
        }
    }
}