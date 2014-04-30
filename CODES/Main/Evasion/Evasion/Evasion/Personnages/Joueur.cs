using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

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

        public Objets.Objets _objet_actuel { get { return objet_actuel;  } }
        public int _bruit { get { return bruit; } }
        public bool _reperer { get { return reperer; } }
        public Capacite _capacite_speciale { get { return capacite_speciale; } }

        public Joueur(ContentManager Content, Matrix view, float aspectRatio) // Constructeur sans parametres
            : base("Joueur", 100, deplacement_t.marche, new Vector3(0, 0, 0), 1, "joueur.3d", genre_t.joueur, Content, view, aspectRatio)
        {
 
        }

        public Joueur(string nom, int vie, deplacement_t deplacement, Vector3 position, int vitesse, string fichier3D, Capacite capacite_speciale, ContentManager Content, Matrix view, float aspectRatio) // Constructeur avec parmetres
            : base(nom, vie, deplacement, position, vitesse, fichier3D, genre_t.joueur, Content, view, aspectRatio)
        {
 
        }

        public void UtiliserCapacite() // Methode pour utiliser la capacite speciale du joueur
        {
            capacite_speciale.Utiliser();
        }

        override public void Deplacer(int angle, direction_t direction) // Methode pour se deplacer 
        {
            
        }
        
        override public void Update(KeyboardState keyboardState, MouseState mouseState, GameTime gametime) // Methode pour charger les donnes
        {
            if(keyboardState.IsKeyDown(Keys.Up))
            {
                Deplacer(0, direction_t.haut);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Deplacer(0, direction_t.bas);
            }
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                Deplacer(0, direction_t.gauche);
            }
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                Deplacer(0, direction_t.droite);
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                UtiliserCapacite();
            }
        }

        override public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb) // Methode pour afficher les donnes
        {
            
        }
    }
}
