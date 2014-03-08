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
        private bool loaded;
        private string path;
        public string Name;
        public Vector2 Pos;
        public Size size;
        public bool Gris;

        public Bouton(string name)
        {
            size = new Size(0, 0);
            loaded = false;
            Name = name;
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
            Name += "_on"; 
            loaded = false;
        }

        public void DeGriser()
        {
            Gris = false;
            Name = Name.Remove(Name.Length-3);
            loaded = false;
        }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb)
        {
            if (!loaded || screen.GraphicsDevice.Viewport.Width != Image.Width)
            {
                Image = screen.Content.Load<Texture2D>(Name);
                loaded = true;
                float pourcentage = Constantes.BUTTON_LENGTH;
                size.Width = (int)(pourcentage*(float)Image.Width);
                size.Height = (int)(pourcentage * (float)Image.Height);
                sb.Draw(Image, Pos, null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), pourcentage, SpriteEffects.None, 0);

            }
            else
                sb.Draw(Image, Pos, Microsoft.Xna.Framework.Color.White);
        }
    }
}
