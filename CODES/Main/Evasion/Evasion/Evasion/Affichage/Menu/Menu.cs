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
using Evasion.Affichage;


namespace Evasion.Affichage.Menu
{
    class Menu
    {
        private Texture2D Image;
        private Bouton[] Elements;
        private string type;
        private Fenetre Fen;
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
                    Elements = new Bouton[4] { new Bouton("New game"), new Bouton("Load game"), new Bouton("Options"), new Bouton("Quit") };
                    Image = screen.Content.Load<Texture2D>("menu_accueil_fond");
                    break;
                case "Pause":
                    Elements = new Bouton[4] { new Bouton("Save"), new Bouton("Quit"), new Bouton("Main menu"), new Bouton("Options") };
                    Image = screen.Content.Load<Texture2D>("menu_accueil_fond");
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
                if (!(Elements[i].Gris) && mouseState.Y >= Elements[i].Pos.Y && mouseState.Y <= Elements[i].Pos.Y + Elements[i].size.Height && mouseState.X >= Elements[i].Pos.X && mouseState.X <= Elements[i].Pos.X + Elements[i].size.Width)
                    Elements[i].Griser();
                else if (Elements[i].Gris && (mouseState.Y < Elements[i].Pos.Y || mouseState.Y > Elements[i].Pos.Y + Elements[i].size.Height || mouseState.X < Elements[i].Pos.X || mouseState.X > Elements[i].Pos.X + Elements[i].size.Width))
                    Elements[i].DeGriser();
                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.Y >= Elements[i].Pos.Y && mouseState.Y <= Elements[i].Pos.Y + Elements[i].size.Height && mouseState.X >= Elements[i].Pos.X && mouseState.X <= Elements[i].Pos.X + Elements[i].size.Width)
                {
                    type=Elements[i].Name.Remove(Elements[i].Name.Length-3);
                    LoadContent(screen);
                }
            }
        }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb)
        {
            int height = screen.GraphicsDevice.Viewport.Height;
            int width = screen.GraphicsDevice.Viewport.Width;
            if (width != Image.Width)
            {
                sb.Draw(Image, new Vector2(0, 0), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), (float)screen.GraphicsDevice.Viewport.Width / (float)Image.Width, SpriteEffects.None, 0);
            }
            else
                sb.Draw(Image, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);

            Vector2 pos = new Vector2(width / 2 - Elements[0].size.Width / 2, height/15);
            Elements[0].Pos = pos;
            Elements[0].Display(screen, sb);
            for (int i = 1; i < Elements.Length; i++)
            {
                pos.X = width / 2 - Elements[i].size.Width / 2;
                pos.Y += Elements[i-1].size.Height+height/10;
                Elements[i].Pos = pos;
                Elements[i].Display(screen, sb);
            }
        }

    }
}
