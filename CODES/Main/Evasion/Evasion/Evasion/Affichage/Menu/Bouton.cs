using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.Drawing;
using Evasion.Affichage;
using Evasion.Son;

namespace Evasion.Affichage.Menu
{
    class Bouton
    {
        private Son.Son son;
        public Texture2D Image;
        public Texture2D Image_on;
        public Texture2D CurrentImage;
        private bool loaded;
        private string path;
        public string Name;
        public Vector2 Pos;
        public Size size;
        public bool Gris;

        public Bouton(string name, Texture2D img1, Texture2D img2)
        {
            Name = name;
            size = new Size(0, 0);
            loaded = false;
            Image = img1;
            Image_on = img2;
            CurrentImage = img1;
            Gris = false;
            son = new Son.Son(Son.ChargerSon.son_bouton);
        }

        public void Press()
        {
            son.Play();
        }

        public void Griser()
        {
            Gris = true;
            CurrentImage = Image_on;
        }

        public void DeGriser()
        {
            Gris = false;
            CurrentImage = Image;
        }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb)
        {
            float pourcentage = Constantes.BUTTON_LENGTH;
            size.Width = (int)(pourcentage * (float)Image.Width);
            size.Height = (int)(pourcentage * (float)Image.Height);
            sb.Draw(CurrentImage, Pos, null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), pourcentage, SpriteEffects.None, 0);
        }
    }
}
