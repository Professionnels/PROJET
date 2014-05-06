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
using System.IO;

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
        public bool multi;
        public bool son;
        public bool appuieSon;
        private List<Evasion.Affichage._3D.Mur> murs;
      //  public bool multi;
        List<Personnage> joueurs;
        List<Ennemi> pnjs;
        private int TAILLE_BLOC;
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
            TAILLE_BLOC = 30;
            Niveau niveau = ChargementNiveau.Load();
            SonAmbiance1 = new Son.Son(Son.ChargerSon.theme1);
            SonAmbiance2 = new Son.Son(Son.ChargerSon.theme2);
            pause = new Son.Son(Son.ChargerSon.son_pause);
            current = SonAmbiance1;
            Fen = fen;
            this.multi = multi;
         //   load(@"C:\Users\epita\Desktop\2\prison_1.txt");
            //for (i = 0; i < nombreJoueurs; i++ )
                //joueurs.Add(new Joueur("Joueur "+i, 100, deplacement_t.marche, new Vector3(0,0,0), 1, "fichier3D", new Capacite("capacite3D")));
            //for (int j=0; j<niveau.Persos.Count; j++)
                //joueurs.Add(niveau.Persos[j]);
        }*/

        public Jeu(Fenetre fen, int nombreJoueurs)
        {
            appuieSon = false;
            son = true;
            TAILLE_BLOC = 30;
            Niveau niveau = ChargementNiveau.Load();
            SonAmbiance1 = new Son.Son(Son.ChargerSon.theme1);
            SonAmbiance2 = new Son.Son(Son.ChargerSon.theme2);
            pause = new Son.Son(Son.ChargerSon.son_pause);
            current = SonAmbiance1;
            Fen = fen;
          //  load(@"C:\Users\epita\Desktop\2\prison_1.txt");

            // charger niveau

            //for (i = 0; i < nombreJoueurs; i++ )
            //joueurs.Add(new Joueur("Joueur "+i, 100, deplacement_t.marche, new Vector3(0,0,0), 1, "fichier3D", new Capacite("capacite3D")));
            //for (int j=0; j<niveau.Persos.Count; j++)
            //joueurs.Add(niveau.Persos[j]);
        }

     /*   public void load(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader str = new StreamReader(file);
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                    switch ((int)str.Read())
                    {
                        case 0:
                            break;
                        case 1:
                            murs.Add(new Evasion.Affichage._3D.Mur());
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        default:
                            break;
                    
                    }
                str.ReadLine();
            }
            file.Close();
        }  */

        public void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            if (Keyboard.GetState().IsKeyUp(Keys.P))
                appuieSon=false;
            if (Keyboard.GetState().IsKeyDown(Keys.P) && !appuieSon)
            {
                if (son)
                {
                    current.Stop();
                    son = false;
                }
                else
                {
                    current = current == SonAmbiance1 ? SonAmbiance2 : SonAmbiance1;
                    current.Play();
                    son = true;
                }
                appuieSon = true;
            }

            // Sons
            if (MediaPlayer.State == MediaState.Stopped && son)
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
