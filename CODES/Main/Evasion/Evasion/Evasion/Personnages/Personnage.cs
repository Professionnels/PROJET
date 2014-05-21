using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Evasion.Personnages;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Evasion.Affichage._3D;
using Evasion.Jeu;

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

    public enum position_t : int
    {
        vide=0,
        decors=1,
        joueur=2
    }

    class Personnage
    {
        public static int tailleX = 20;
        public static int tailleZ = 15;

        protected Niveau niveau;
        protected int vie;
        protected float vitesse; 
        protected genre_t genrePerso;
        protected deplacement_t deplacement;
        protected string nom;
        protected Objets.Objets objet;
        protected Vector3 position;
        protected Vector3 rotation;

        public Vector3 Position { get { return position; } set { position = value; } }
        public Vector3 Rotation { get { return rotation; } set { rotation = value; } }
        public int _vie { get { return vie; } }
        public float _vitesse { get { return vitesse; } }
        public genre_t _genrePerso { get { return genrePerso; } }
        public deplacement_t _deplacement { get { return deplacement; } }
        public string _nom { get { return nom; } }
        public Objets.Objets _objet { get { return objet; } }

        public Personnage(ContentManager Content, GraphicsDeviceManager graphics, string nom, int vie, deplacement_t deplacement, Vector3 position, float vitesse, genre_t genrePerso, Matrix view, float aspectRatio, Niveau niveau) // Constructeur avec parametres
        {
            this.niveau = niveau;
            this.position = position;
            this.nom = nom;
            this.vie = vie;
            this.vitesse = vitesse;
            this.genrePerso = genrePerso;
            this.objet = new Objets.Objets();
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
        }

        virtual public void Display(Camera camera) // Methode pour afficher les donnees
        {
        }
    }
}
