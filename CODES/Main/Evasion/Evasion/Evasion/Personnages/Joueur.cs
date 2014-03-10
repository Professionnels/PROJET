using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Evasion.Personnages
{
    public enum direction_t : int
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

        public Joueur()
            : base("Joueur", 100, deplacement_t.marche, new Vector3(0,0,0), 1, 0, "joueur.3d", genre_t.joueur)
        {
 
        }

        public Joueur(string nom, int vie, deplacement_t deplacement, Vector3 position, int vitesse, int identifiant, string fichier3D, Capacite capacite_speciale)
            :base(nom, vie, deplacement, position, vitesse, identifiant, fichier3D, genre_t.joueur)
        {
 
        }

        public void UtiliserCapacite()
        {
            capacite_speciale.Utiliser();
        }

        public void Deplacer(int angle, direction_t direction)
        {
        
        }
        
        public void Update(KeyboardState keyboardState, MouseState mouseState)
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
        }

        public void Display(SpriteBatch sb)
        {
 
        }
    }
}
