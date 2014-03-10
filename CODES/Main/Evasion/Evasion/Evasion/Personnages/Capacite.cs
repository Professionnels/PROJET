using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion.Personnages
{
    class Capacite
    {
        protected string fichier3D;

        virtual public void Utiliser()
        {

        }
        
        public Capacite(string fichier3D)
        {
            this.fichier3D = fichier3D;
        }

    }
    class CoupDePoing : Capacite
    {
        public override void Utiliser()
        {
            
        }

        public CoupDePoing(string fichier3D)
            :base(fichier3D)
        {
            this.fichier3D = fichier3D;
        }
    }
}
