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

namespace Evasion.Affichage.Menu
{
    class Bouton
    {
        private Menu menu;
        private Texture2D Image;
        private bool loaded;
        private string path;
        public string Name;
        public Vector2 Pos;
        public Size size;
        public bool Gris;

        public Bouton(string name)
        {
            size = new Size(850, 60);
            loaded = false;
            Name = name;
            Gris = false;
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

        public void LoadContent(ContentManager cm, SpriteBatch sb)
        {
            path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\GRAPHISMES\\Images\\Menu";
        }

        public void Display(ContentManager cm, SpriteBatch sb)
        {
            if (!loaded)
            {
                Image = cm.Load<Texture2D>(path + Name + ".bmp");
                loaded = true;
            }
            sb.Draw(Image, Pos, Microsoft.Xna.Framework.Color.Transparent);
        }
    }
}
