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
                    Elements = new Bouton[4] { new Bouton("New game", ChargerImages.bouton_nouveau_jeu, ChargerImages.bouton_nouveau_jeu_on), new Bouton("Load game", ChargerImages.bouton_charger_jeu, ChargerImages.bouton_charger_jeu_on), new Bouton("Options", ChargerImages.bouton_options, ChargerImages.bouton_options_on), new Bouton("Quit", ChargerImages.bouton_quitter, ChargerImages.bouton_quitter_on) };
                    Image = ChargerImages.menu_accueil;
                    break;
                case "Pause":
                    Elements = new Bouton[5] { new Bouton("Reprendre", ChargerImages.bouton_reprendre, ChargerImages.bouton_reprendre_on), new Bouton("Save", ChargerImages.bouton_sauvegarder, ChargerImages.bouton_sauvegarder_on), new Bouton("Main menu", ChargerImages.bouton_menu_principal, ChargerImages.bouton_menu_principal_on), new Bouton("Options", ChargerImages.bouton_options, ChargerImages.bouton_options_on), new Bouton("Quit", ChargerImages.bouton_quitter, ChargerImages.bouton_quitter_on) };
                    Image = ChargerImages.menu_accueil;
                    break;
                case "Reprendre":
                    Fen.LoadContent(Content_t.LoadGame);
                    Fen.ok = true;
                    break;
                case "Quit":
                    Fen.LoadContent(Content_t.Quit);
                    break;
                case "Save":
                    //Fen.LoadContent(Content_t.Save);
                    break;
                case "New game":
                    Fen.LoadContent(Content_t.NewGame);
                    break;
                case "Load game":
                    //Fen.LoadContent(Content_t.LoadGame);
                    break;
                case "Options":
                    //Fen.LoadContent(Content_t.Options);
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
                    type=Elements[i].Name;
                    if (Elements[i].Name == "New game" || Elements[i].Name == "Reprendre" || Elements[i].Name == "Main menu")
                        Elements[i].Press();
                    LoadContent(screen);
                }
            }
        }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb)
        {
            int height = screen.GraphicsDevice.Viewport.Height;
            int width = screen.GraphicsDevice.Viewport.Width;
            sb.Draw(Image, new Vector2(0, 0), null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), (float)screen.GraphicsDevice.Viewport.Width / (float)Image.Width, SpriteEffects.None, 0);

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
