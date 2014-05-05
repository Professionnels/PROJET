using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Evasion.Affichage;
using Microsoft.Xna.Framework.Media;
using Evasion.Personnages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Evasion.Jeu
{
    class Jeu
    {
        private Son.Son SonAmbiance1;
        private Son.Son SonAmbiance2;
        private Son.Son current;
        private Son.Son pause;
        private Fenetre Fen;
        private Niveau niveau;
      //  public bool multi;
        List<Personnage> joueurs;
       /* Viewport defaultview;
        Viewport leftview;
        Viewport rightview;
        Evasion.Affichage._3D.Camera cameratwo;
        Evasion.Affichage._3D.Perso_Model michael;
        Evasion.Affichage._3D.Perso_Model bellick;
        Evasion.Affichage._3D.Mur murchangeant;
        Evasion.Affichage._3D.Sol solChangeant;
        Evasion.Affichage._3D.Mur Tmur; */

    /*   public Jeu(Fenetre fen, int nombreJoueurs, bool multi)
        {
            Niveau niveau = ChargementNiveau.Load();
            SonAmbiance1 = new Son.Son(Son.ChargerSon.theme1);
            SonAmbiance2 = new Son.Son(Son.ChargerSon.theme2);
            pause = new Son.Son(Son.ChargerSon.son_pause);
            current = SonAmbiance1;
            Fen = fen;
            this.multi = multi;
            //for (i = 0; i < nombreJoueurs; i++ )
                //joueurs.Add(new Joueur("Joueur "+i, 100, deplacement_t.marche, new Vector3(0,0,0), 1, "fichier3D", new Capacite("capacite3D")));
            //for (int j=0; j<niveau.Persos.Count; j++)
                //joueurs.Add(niveau.Persos[j]);
        }*/

        public Jeu(Fenetre fen, int nombreJoueurs)
        {
            Niveau niveau = ChargementNiveau.Load();
            SonAmbiance1 = new Son.Son(Son.ChargerSon.theme1);
            SonAmbiance2 = new Son.Son(Son.ChargerSon.theme2);
            pause = new Son.Son(Son.ChargerSon.son_pause);
            current = SonAmbiance1;
            Fen = fen;

            // charger niveau

            //for (i = 0; i < nombreJoueurs; i++ )
            //joueurs.Add(new Joueur("Joueur "+i, 100, deplacement_t.marche, new Vector3(0,0,0), 1, "fichier3D", new Capacite("capacite3D")));
            //for (int j=0; j<niveau.Persos.Count; j++)
            //joueurs.Add(niveau.Persos[j]);
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState)
        {

            // Sons
            if (MediaPlayer.State == MediaState.Stopped)
            {
                current.Play();
                current = current == SonAmbiance1 ? SonAmbiance2 : SonAmbiance1;
            }

            // Touches
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                pause.Play();
                Fen.LoadContent(Content_t.Menu);
            }

            //for (int i = 0; i < joueurs.Count(); i++)
                //joueurs[i].Update(keyboardState, mouseState);

        }

        public void Save()
        { }

        public void DisplaySaveProgressing()
        { }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb, GraphicsDevice gd, ContentManager cm)
        {
            /*
            if (multi)
            {
                defaultview = gd.Viewport;
                leftview = defaultview;
                rightview = defaultview;
                leftview.Width = leftview.Width / 2;
                rightview.Width = rightview.Width / 2 - 9;
                rightview.X = leftview.Width + 9;
                bellick = new Affichage._3D.Perso_Model(cm, new Vector3(40, 0, -20), 100, aspectRatio, gd, 2);
            }
            //for (int i = 0; i < joueurs.Count(); i++)
                //joueurs[i].Display(screen, sb);
       
            */
        }
    }
}
