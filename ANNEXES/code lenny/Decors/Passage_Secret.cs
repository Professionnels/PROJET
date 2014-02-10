using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion.Decors
{
    class Passage_Secret
    {
        private string fichier3D;
        private int type;
        private int identifiant;
        private int etat;

        public Passage_Secret() { }
        public Passage_Secret(string fichier3D, int type, int identifiant)
        {
            this.fichier3D = fichier3D;
            this.type = type;
            this.identifiant = identifiant;
            this.etat = 0;
        }
    }
}
