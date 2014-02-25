using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion.Decors
{
    class Porte
    {
        private string fichier3D;
        private int type;
        private int identifiant;
        private int etat;

        public Porte() { }
        public Porte(string fichier3D, int type,int identifiant) 
        {
            this.fichier3D = fichier3D;
            this.type = type;
            this.identifiant = identifiant;
            this.etat = 0;
        }
    }
}
