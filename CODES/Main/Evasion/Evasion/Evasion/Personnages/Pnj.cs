using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Evasion.Jeu;

namespace Evasion.Personnages
{
    class Pnj : Personnage
    {
        private string dialogue1;
        private string dialogue2;
        private string dialogue3;
        private int objet_a_donner;

        public string _dialogue1 { get { return dialogue1; } }
        public string _dialogue2 { get { return dialogue2; } }
        public string _dialogue3 { get { return dialogue3; } }
        public int _objet_a_donner { get { return objet_a_donner; } } 

    public Pnj(ContentManager Content, GraphicsDeviceManager graphics, Matrix view, float aspectRatio, Niveau niveau)
            : base(Content, graphics, "Pnj", 100, deplacement_t.marche, new Vector3(0,0,0), 1f, genre_t.pnj, view, aspectRatio, niveau)
        {
 
        }

    public Pnj(string nom, int vie, deplacement_t deplacement, Vector3 position, int vitesse, int id, string fichier3D, string dialogue1, string dialogue2, string dialogue3, int objet_a_donner, ContentManager Content, GraphicsDeviceManager graphics, Matrix view, float aspectRatio, Niveau niveau)
            : base(Content, graphics, nom, vie, deplacement, position, vitesse, genre_t.pnj, view, aspectRatio, niveau) // fichier mouv.txt
        {
            this.dialogue1 = dialogue1;
            this.dialogue2 = dialogue2;
            this.dialogue3 = dialogue3;
        }

        public void Action()
        {

        }

        public string Parler()
        {
            return "";
            //if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                //this.Exit();
            //if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                //this.
                
        }
        public int Echanger() { return 0; }
    }
}