using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Evasion.Personnages;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Evasion
{
    class Ennemi : Personnage
    {
        private int argent_recompense;
        private int timer_reperer;
        private int champs_vision;
        private bool assome;
        private int temps_assome;
        private bool mobile;
        private Joueur cible;

        public int _argent_recompense { get { return argent_recompense; } }
        public Joueur _cible { get { return cible; } }
        public int _timer_reperer { get { return timer_reperer; } }
        public int _champs_vision { get { return champs_vision; } }
        public bool _assome { get { return assome; } }
        public int _temps_assome { get { return temps_assome; } }
        public bool _mobile { get { return mobile; } }

        public Ennemi(ContentManager Content, Matrix view, float aspectRatio)
            : base("Ennemi", 100, deplacement_t.marche, new Vector3(0, 0, 0), 1, "fichier3D", genre_t.ennemi, Content, view, aspectRatio)
        {
 
        }

        public Ennemi(string nom, int vie, deplacement_t deplacement, Vector3 position, int vitesse, int id, string fichier3D, int argent_recompense, int timer_reperer, int champs_vision, bool mobile, ContentManager Content, Matrix view, float aspectRatio)
            : base(nom, vie, deplacement, position, vitesse, fichier3D, genre_t.ennemi, Content, view, aspectRatio) // fichier mouv.txt
        {
            this.argent_recompense = argent_recompense;
            this.timer_reperer = timer_reperer;
            this.champs_vision = champs_vision;
            assome = false;
            temps_assome = 0;
            this.mobile = mobile;
        }

        private void Attaquer()
        {
            //int distance;
            //switch (distance)
        }

        private bool Reperer()
        {
            return false;
        }

        private void Alerter()
        {
            
        }

        override public void Deplacer(int angle, direction_t direction)
        {

        }

        public void Mouvement()
        {
            // mouv.txt
        }

        override public void Update(KeyboardState keyboardState, MouseState mouseState, GameTime gametime)
        {
            if (Reperer())
            {
                Alerter();
                Attaquer();
            }
            Mouvement();
        }

        override public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb)
        {

        }
    }
}
