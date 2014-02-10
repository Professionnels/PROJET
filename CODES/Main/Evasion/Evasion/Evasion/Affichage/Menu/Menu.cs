using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.IO;
using System.Drawing;
using Microsoft.Xna.Framework.Input;


namespace Evasion.Affichage.Menu
{
    class Menu
    {
        private Texture2D Image;
        private char Id;
        private Bouton[] Elements;
        private string type;
        private Fenetre Fen;
        private string path;
        private Microsoft.Xna.Framework.Game screen;

        public Menu(string type, Fenetre Fen)
        {
            this.type = type;
            this.Fen = Fen;
        }

        public void LoadContent(Microsoft.Xna.Framework.Game screen)
        {
            this.screen = screen;
            switch (type)
            {
                case "Main menu":
                    Elements = new Bouton[4] { new Bouton("New game"), new Bouton("Load game"), new Bouton("Quit"), new Bouton("Options") };
                    path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName + "\\GRAPHISMES\\Images\\Menu\\Menu_accueil_fond.bmp");
                    Image = screen.Content.Load<Texture2D>(path);
                    break;
                case "Pause":
                    Elements = new Bouton[4] { new Bouton("Save"), new Bouton("Quit"), new Bouton("Main menu"), new Bouton("Options") };
                    path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName + "\\GRAPHISMES\\Images\\Menu\\Menu_accueil_fond.bmp");
                    Image = screen.Content.Load<Texture2D>(path);
                    break;
                case "Quit":
                    Fen.LoadContent(Content_t.Quit);
                    break;
                case "Save":
                    Fen.LoadContent(Content_t.Save);
                    break;
                case "New game":
                    Fen.LoadContent(Content_t.NewGame);
                    break;
                case "Load game":
                    Fen.LoadContent(Content_t.LoadGame);
                    break;
                case "Options":
                    Fen.LoadContent(Content_t.Options);
                    break;
            }
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            for (int i = 0; i < Elements.Length; i++)
            {
                if (!(Elements[i].Gris) && mouseState.Y >= Elements[i].Pos.Y && mouseState.Y <= Elements[i].Pos.Y + Elements[i].size.Height)
                    Elements[i].Griser();
                else if (Elements[i].Gris && (mouseState.Y < Elements[i].Pos.Y || mouseState.Y > Elements[i].Pos.Y + Elements[i].size.Height))
                    Elements[i].DeGriser();
                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.Y >= Elements[i].Pos.Y && mouseState.Y <= Elements[i].Pos.Y + Elements[i].size.Height)
                {
                    type=Elements[i].Name;
                    LoadContent(screen);
                }
            }
        }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb)
        {
            sb.Draw(Image, new Vector2(0,0), Microsoft.Xna.Framework.Color.Transparent);

            int height = screen.GraphicsDevice.Viewport.Height;
            int width = screen.GraphicsDevice.Viewport.Width;
            Vector2 pos = new Vector2(0, height / (Elements.Length*2));
            for (int i = 0; i < Elements.Length; i++)
            {
                pos.Y += screen.GraphicsDevice.Viewport.Height / (Elements.Length+1);
                Elements[i].Pos = pos;
                int pourcentage = 100*height/Elements[i].size.Height;
                Elements[i].size = new Size(width, Elements[i].size.Height + pourcentage*Elements[i].size.Height/100);
                Elements[i].Display(screen.Content, sb);
            }
        }

    }
}
