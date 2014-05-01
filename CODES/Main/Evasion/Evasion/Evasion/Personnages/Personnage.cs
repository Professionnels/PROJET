using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Evasion.Personnages;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Evasion
{
    public enum deplacement_t : int // Enumeration pour les differents modes de deplacements
    {
        marche = 0,
        courir,
        sneak,
        accroupi
    }

    public enum genre_t : int // Enumeration pour les differents genre de personnage;
    {
        joueur = 0,
        ennemi,
        pnj
    }

    class Personnage
    {
        protected int vie;
        protected int vitesse; 
        protected genre_t genrePerso;
        protected deplacement_t deplacement;
        protected string nom;
        protected string fichier3D; // emplacement modele 3D du perso
        protected Objets.Objets objet;
        protected Affichage._3D.Perso_Model model3D;

        public int _vie { get { return vie; } }
        public int _vitesse { get { return vitesse; } }
        public genre_t _genrePerso { get { return genrePerso; } }
        public deplacement_t _deplacement { get { return deplacement; } }
        public string _nom { get { return nom; } }
        public string _fichier3D { get { return fichier3D; } }
        public Objets.Objets _objet { get { return objet; } }
        public Affichage._3D.Perso_Model _model3D { get { return model3D; } } 

        public Personnage()  // Constructeur sans parametres
        {

        }

        public Personnage(string nom, int vie, deplacement_t deplacement, Vector3 position, int vitesse, string fichier3D, genre_t genrePerso, ContentManager content, Matrix view, float aspectRatio ) // Constructeur avec parametres
        {
            this.nom = nom;
            this.vie = vie;
            this.vitesse = vitesse;
            this.genrePerso = genrePerso;
            this.fichier3D = nom;
            this.objet = new Objets.Objets();
            //this.model3D = new Affichage._3D.Perso_Model(content, Vector3.Zero, view, aspectRatio);
        }

        public void ChangerDeplacement(deplacement_t deplacement) // Methode pour changer le mode de deplacement
        {
            this.deplacement = deplacement;
        }

        public void UtiliserObjet() // Methode pour utiliser un objet
        {
            objet.Utiliser();
        }

        public int ModifierVie(int ajout) // Methode pour modifier la vie
        {
            return vie + ajout;
        }

        virtual public void Deplacer(int angle, direction_t direction) // Methode pour se deplacer
        {
            
        }

        virtual public void Update(KeyboardState keyboardState, MouseState mouseState, GameTime gametime) // Methode pour charger les donnees
        {
            //model3D.UpdatePosition(gametime);
        }

        virtual public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb) // Methode pour afficher les donnees
        {
            model3D.draw();
        }
    }
}
