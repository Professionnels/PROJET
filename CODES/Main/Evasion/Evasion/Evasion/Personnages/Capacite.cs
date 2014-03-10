using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion.Personnages
{
    //gros boloss ce khalis
    class Capacite
    {
        protected string fichier3D;

        virtual public void Utiliser() // Methode pour utiliser la capacite
        {

        }
        
        public Capacite(string fichier3D) // constructeur avec parametres
        {
            this.fichier3D = fichier3D;
        }

    }
    class CoupDePoing : Capacite
    {
        public override void Utiliser() // Methode pour utiliser l'attaque
        {
            
        }

        public CoupDePoing(string fichier3D) // Constructeur avec parametres
            :base(fichier3D)
        {
            this.fichier3D = fichier3D;
        }
    }
}
