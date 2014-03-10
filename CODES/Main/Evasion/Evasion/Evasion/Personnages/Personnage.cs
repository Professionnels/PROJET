using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion
{
    class Personnage
    {
        private int vie;
        private int vitesse; 
        private int identifiant;
        private int type;
        private int genrePerso;
        private int accroupi;
        private int marche;
        private int court;
        private int sneak;
        private string nom;
        private string fichier3D; // emplacement modele 3D du perso
        private Objets.Objets objet;

        public int _vie { get { return vie; } }
        public int _vitesse { get { return vitesse; } }
        public int _identifiant { get { return identifiant; } } 
        public int _type { get { return type; } }
        public int _genrePerso { get { return genrePerso; } }
        public int _accroupi { get { return accroupi; } }
        public int _marche { get { return marche; } }
        public int _court { get { return court; } }
        public int _sneak { get { return sneak; } }
        public string _nom { get { return nom; } }
        public string _fichier3D { get { return fichier3D; } }
        public Objets.Objets _objet { get { return objet; } } 

        public Personnage() {} // Constructeur sans parametres

        public Personnage(int vie, int vitesse, int identifiant, int type, int genrePerso, int accroupi, int marcher, int courrir, int sneak, string nom, string fichier3D, Objets.Objets objet) // Constructeur avec parametres
        {
            this.vie = vie;
            this.vitesse = vitesse;
            this.identifiant = identifiant;
            this.type = type;
            this.genrePerso = genrePerso;
            this.accroupi = accroupi;
            this.marche = marcher;
            this.court = courrir;
            this.sneak = sneak;
            this.nom = nom;
            this.fichier3D = fichier3D;
            this.objet = objet;
        }
        
        private void accroupir(int accroupi) // Methode pour s'accroupir
        {
            accroupi = accroupi ^ 1;
        }

        private void marcher(int marche) // Methode pour marcher
        {
            marche = marche ^ 1;
        }
        
        private void courrir(int court) // Methode pour courrir
        {
            court = court ^ 1;
        }
        
        private void sneaker(int sneak) // Methode pour marcher lentement
        {
            sneak = sneak ^ 1;
        }

        private void utiliserObjet() // Methode pour utiliser un objet
        {
            objet.Utiliser();
        }

        private int modifierVie(int ajout) // Methode pour modifier la vie
        {
            return vie + ajout;
        }
    }
}
