using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Evasion.Jeu;
using Evasion.Affichage._3D;

namespace Evasion.Personnages
{
    public enum direction_t : int // Enumeration pour les differents directions possibles
    {
         gauche = 0,
         droite,
         haut,
         bas
    }

    class Joueur : Personnage
    {
        
        private Objets.Objets objet_actuel;
        private int bruit;
        private bool reperer;
        private Capacite capacite_speciale;
        private Perso_Model image;

        public Objets.Objets _objet_actuel { get { return objet_actuel;  } }
        public int _bruit { get { return bruit; } }
        public bool _reperer { get { return reperer; } }
        public Capacite _capacite_speciale { get { return capacite_speciale; } }

        public Joueur(ContentManager Content, GraphicsDeviceManager graphics, Matrix view, float aspectRatio, Niveau niveau) // Constructeur sans parametres
            : base(Content, graphics,  "Joueur", 100, deplacement_t.marche, new Vector3(0, 0, 0), 1, genre_t.joueur, view, aspectRatio, niveau)
        {
            image = new Perso_Model(Content, position, view, aspectRatio, graphics, 1);
        }

        public Joueur(string nom, int vie, deplacement_t deplacement, Vector3 position, float vitesse, Capacite capacite_speciale, ContentManager Content, GraphicsDeviceManager graphics, Matrix view, float aspectRatio, Niveau niveau) // Constructeur avec parmetres
            : base(Content, graphics, nom, vie, deplacement, position, vitesse, genre_t.joueur, view, aspectRatio, niveau)
        {
            image = new Perso_Model(Content, position, view, aspectRatio, graphics, 1);
        }

        public void UtiliserCapacite() // Methode pour utiliser la capacite speciale du joueur
        {
            capacite_speciale.Utiliser();
        }

        //override public void Deplacer(int angle, direction_t direction) // Methode pour se deplacer 
        //{
            
        //}
        
        override public void Update(KeyboardState keyboardState, MouseState mouseState, GameTime gametime) // Methode pour charger les donnes
        {
            image.UpdatePosition(vitesse, niveau, gametime);
            position = image.getPosition();
            rotation = image.getRotation();
            //if (keyboardState.IsKeyDown(Keys.W))
            //{
            //    UtiliserCapacite();
            //}
        }

        override public void Display(Camera camera) // Methode pour afficher les donnes
        {
            image.draw(camera);
        }
    }
}
